using Nethereum.RPC.Eth.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using WPFTrackingBC.Models;
using WPFTrackingBC.ViewModel;

namespace WPFTrackingBC
{
    /// <summary>
    /// Interaction logic for TrackingUserControl.xaml
    /// </summary>
    public partial class TrackingUserControl : UserControl
    {
        TrackingViewModel vm = new TrackingViewModel();
        ObservableCollection<TrackingDetails> TrackingDetails = new ObservableCollection<TrackingDetails>();
        ApprovalsDetails selectedItem;
        public ShipmentDetails Container;
        bool Approved;
        bool weighingApproved;
        bool paymentSuccess;
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        public TrackingUserControl()
        {
            InitializeComponent();
            this.Loaded += TrackingUserControl_Loaded;
        }

        private void TrackingUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            SetSupplierAndBank();
            btnCheckWeight.IsEnabled = false;
            btnRejectWeighing.IsEnabled = false;
            btnExport.IsEnabled = false;
            btnReject.IsEnabled = false;
            vm.Approvalsdetail = new ObservableCollection<ApprovalsDetails>();
            stksrc.DataContext = Container;
            StackApprover.DataContext = Container;
            txtcid.Text = Container.Id;
            txtcname.Text = Container.ContainerName;
            txtUserName.Text = App.UserName;
            txtsource.Text += " " +Container.Source;
            txtdestination.Text += " "+Container.Destination;
            gridApprover.DataContext = Container;

            //EnableControls();
            if (App.UserType == UserType.Initiater)
            {
                stkWeight.Visibility = Visibility.Collapsed;
                stkAppprover.Visibility = Visibility.Collapsed;
                stkExporter.Visibility = Visibility.Collapsed;
            }
            else if (App.UserType == UserType.Weighing)
            {
                stkAppprover.Visibility = Visibility.Collapsed;
                stkWeight.Visibility = Visibility.Visible;
                stkExporter.Visibility = Visibility.Collapsed;
            }
            else if(App.UserType==UserType.ExportAuthority)
            {
                stkAppprover.Visibility = Visibility.Collapsed;
                stkWeight.Visibility = Visibility.Collapsed;
                stkExporter.Visibility = Visibility.Visible;
            }
            else
            {
                stkWeight.Visibility = Visibility.Collapsed;
                stkExporter.Visibility = Visibility.Collapsed;
                stkAppprover.Visibility = Visibility.Visible;
            }
            //vm.Approvalsdetail.Add(new ApprovalsDetails() { Document = "Document 1", _status = 0, Url = "http://idtp376/Pdf/Ethereum%20Consortium%20Blockchain%20in%20Azure%20Marketplace.pdf" });
            //vm.Approvalsdetail.Add(new ApprovalsDetails() { Document = "Document 2", _status = 0, Url = "http://idtp376/Pdf/Ethereum%20Multi-Member%20Consortium%20Network.pdf" });
            //vm.Approvalsdetail.Add(new ApprovalsDetails() { Document = "Document 3", _status = 0, Url = "http://idtp376/Pdf/GST_B2B_INVOICE_CASES-CF.pdf" });

            //brApprover1.DataContext = vm.Approvalsdetail[0];
            //Approver1br.DataContext = vm.Approvalsdetail[0];
            //Approver2br.DataContext = vm.Approvalsdetail[1];
            //Approver3br.DataContext = vm.Approvalsdetail[2];
            //brApprover2.DataContext = vm.Approvalsdetail[1];
            //brApprover3.DataContext = vm.Approvalsdetail[2];
            listDoc.ItemsSource = vm.Approvalsdetail;
            listDoc.SelectedIndex = 0;
            listTrack.ItemsSource = TrackingDetails;
            TrackApprovals();
            TrackShipment();
            SetStatus();

           
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 10);
            dispatcherTimer.Start();
            
            
        }

        private async void SetSupplierAndBank()
        {
            //var Supplierfunc = App.mainWindow.shipmentContract.GetFunction("Supplier");
            var Supplier = await APICall.GetContainerSupplier(Container.Id);
            txtsupplier.Text += " : " + Supplier;
            //var Bankfunc = App.mainWindow.paymentContract.GetFunction("Bank");
            //var Bank = await Bankfunc.CallAsync<string>(Supplier);
            //txtbank.Text += " : " + Bank;
        }

        private void EnableControls()
        {

            switch (App.UserType)
            {
                case UserType.Excise:
                    vm.Approvalsdetail.Add(new ApprovalsDetails() {DocId=1, Document = "Excise Form", _status = 0, Url = "http://idtp376/Pdf/Ethereum%20Consortium%20Blockchain%20in%20Azure%20Marketplace.pdf" });
                    vm.Approvalsdetail.Add(new ApprovalsDetails() {DocId=2, Document = "Packaging List", _status = 0, Url = "http://idtp376/Pdf/Ethereum%20Multi-Member%20Consortium%20Network.pdf" });
                    break;
                case UserType.Weighing:
                    vm.Approvalsdetail.Add(new ApprovalsDetails() { DocId = 3, Document = "VGM", _status = 0, Url = "http://idtp376/Pdf/ASF-VGM-Declaration-Form.pdf" });
                    break;
                case UserType.Custom:
                    vm.Approvalsdetail.Add(new ApprovalsDetails() { DocId = 4, Document = "Custom Form", _status = 0, Url = "http://idtp376/Pdf/GST_B2B_INVOICE_CASES-CF.pdf" });
                    break;
                default:
                    vm.Approvalsdetail.Add(new ApprovalsDetails() { DocId = 1, Document = "Excise Form", _status = 0, Url = "http://idtp376/Pdf/Ethereum%20Consortium%20Blockchain%20in%20Azure%20Marketplace.pdf" });
                    vm.Approvalsdetail.Add(new ApprovalsDetails() { DocId = 2, Document = "Packaging List", _status = 0, Url = "http://idtp376/Pdf/Ethereum%20Multi-Member%20Consortium%20Network.pdf" });
                    vm.Approvalsdetail.Add(new ApprovalsDetails() { DocId = 3, Document = "VGM", _status = 0, Url = "http://idtp376/Pdf/ASF-VGM-Declaration-Form.pdf" });
                    vm.Approvalsdetail.Add(new ApprovalsDetails() { DocId = 4, Document = "Custom Form", _status = 0, Url = "http://idtp376/Pdf/GST_B2B_INVOICE_CASES-CF.pdf" });
                    break;
            }
        }

        private void SetStatus()
        {
            switch(Container.Status)
            {
                case Status.Initializing:
                case Status.PendingSupplierApproval:
                    brsource.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD42A"));
                    brApproval.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#074A63"));
                    brWeighing.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#074A63"));
                    brExcies.Background= new SolidColorBrush((Color)ColorConverter.ConvertFromString("#074A63"));
                    brDestination.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#074A63"));
                    break;
                case Status.Initiated:
                    border1.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B1CB21"));
                    brsource.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B1CB21"));
                    brApproval.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD42A"));
                    brWeighing.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#074A63"));
                    brExcies.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#074A63"));
                    brDestination.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#074A63"));
                    break;
                case Status.ExciseApproved:
                    if (prg.Visibility == Visibility.Collapsed && !weighingApproved)
                    {
                        btnCheckWeight.IsEnabled = true;
                        btnRejectWeighing.IsEnabled = true;
                    }
                    else
                    {
                        btnCheckWeight.IsEnabled = false;
                        btnRejectWeighing.IsEnabled = false;
                    }
                    border1.Background=new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B1CB21"));
                    border2.Background=new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B1CB21"));
                    brsource.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B1CB21"));
                    brApproval.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B1CB21"));
                    brWeighing.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD42A"));
                    brExcies.Background= new SolidColorBrush((Color)ColorConverter.ConvertFromString("#074A63"));
                    brDestination.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#074A63"));
                    break;
                case Status.WeighingApproved:
                    border1.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B1CB21"));
                    border2.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B1CB21"));
                    border3.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B1CB21"));
                    brsource.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B1CB21"));
                    brApproval.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B1CB21"));
                    brWeighing.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B1CB21"));
                    brExcies.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD42A"));
                    brDestination.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#074A63"));
                    break;
                case Status.CustomApproved:
                    btnExport.IsEnabled = true;
                    btnReject.IsEnabled = true;
                    border1.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B1CB21"));
                    border2.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B1CB21"));
                    border3.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B1CB21"));
                    border4.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B1CB21"));
                    brsource.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B1CB21"));
                    brApproval.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B1CB21"));
                    brWeighing.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B1CB21"));
                    brExcies.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B1CB21"));
                    brDestination.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD42A"));
                    break;
                case Status.Rejected:
                    border1.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B1CB21"));
                    border2.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B1CB21"));
                    border3.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B1CB21"));
                    border4.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B1CB21"));
                    border5.Background = new SolidColorBrush(Colors.LightGray);
                    border6.Background = new SolidColorBrush(Colors.LightGray);
                    brsource.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B1CB21"));
                    brApproval.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B1CB21"));
                    brWeighing.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B1CB21"));
                    brExcies.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B1CB21"));
                    brDestination.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF2A55"));
                    brBank.Background = new SolidColorBrush(Colors.LightGray);
                    brSupplier.Background = new SolidColorBrush(Colors.LightGray);
                    break;
                case Status.WeighingRejected:
                    border1.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B1CB21"));
                    border2.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B1CB21"));
                    border3.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF2A55"));
                    border4.Background = new SolidColorBrush(Colors.LightGray);
                    border5.Background = new SolidColorBrush(Colors.LightGray);
                    border6.Background = new SolidColorBrush(Colors.LightGray);
                    brsource.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B1CB21"));
                    brApproval.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B1CB21"));
                    brWeighing.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF2A55"));
                    brExcies.Background = new SolidColorBrush(Colors.LightGray);
                    brDestination.Background = new SolidColorBrush(Colors.LightGray);
                    brBank.Background = new SolidColorBrush(Colors.LightGray);
                    brSupplier.Background = new SolidColorBrush(Colors.LightGray);
                    break;
                case Status.CustomRejected:
                    border1.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B1CB21"));
                    border2.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B1CB21"));
                    border3.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B1CB21"));
                    border4.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF2A55"));
                    border5.Background = new SolidColorBrush(Colors.LightGray);
                    border6.Background = new SolidColorBrush(Colors.LightGray);
                    brsource.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B1CB21"));
                    brApproval.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B1CB21"));
                    brWeighing.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B1CB21"));
                    brExcies.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF2A55"));
                    brDestination.Background = new SolidColorBrush(Colors.LightGray);
                    brBank.Background = new SolidColorBrush(Colors.LightGray);
                    brSupplier.Background = new SolidColorBrush(Colors.LightGray);
                    break;
                case Status.ExciseRejected:
                    border1.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B1CB21"));
                    border2.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF2A55"));
                    border3.Background = new SolidColorBrush(Colors.LightGray);
                    border4.Background = new SolidColorBrush(Colors.LightGray);
                    border5.Background = new SolidColorBrush(Colors.LightGray);
                    border6.Background = new SolidColorBrush(Colors.LightGray);
                    brsource.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B1CB21"));
                    brApproval.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF2A55"));
                    brWeighing.Background = new SolidColorBrush(Colors.LightGray);
                    brExcies.Background = new SolidColorBrush(Colors.LightGray);
                    brDestination.Background = new SolidColorBrush(Colors.LightGray);
                    brBank.Background = new SolidColorBrush(Colors.LightGray);
                    brSupplier.Background = new SolidColorBrush(Colors.LightGray);
                    break;
                case Status.GatedIn:
                    border1.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B1CB21"));
                    border2.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B1CB21"));
                    border3.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B1CB21"));
                    border4.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B1CB21"));
                    border5.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B1CB21"));
                    border6.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B1CB21"));
                    brsource.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B1CB21"));
                    brApproval.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B1CB21"));
                    brWeighing.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B1CB21"));
                    brExcies.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B1CB21"));
                    brDestination.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B1CB21"));
                    brBank.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B1CB21"));
                    brSupplier.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B1CB21"));
                    break;
                

            }
        }

        private async void CalculatePayment()
        {
            var detail =await APICall.GetContainerShipmentDetail(Container.Id);
            if (detail != null)
            {
                PaymentDetails details = new PaymentDetails()
                {
                    ContainerId = Container.Id,
                    ContainerName = Container.ContainerName,
                    Quantity = detail.Quantity,
                    SupplierName = detail.SupplierName,
                    Unit = detail.Quantity
                };
               await  APICall.CalculatePayment(details.ContainerId, details);
            }

        }

        private async void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            await TrackShipment();
            await TrackApprovals();
        }

        private void listDoc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var doc = (ApprovalsDetails)listDoc.SelectedItem;
            txtdocname.Text = ((ApprovalsDetails)listDoc.SelectedItem).Document;
            webdoc.Source = new Uri(((ApprovalsDetails)listDoc.SelectedItem).Url);
            if (doc._status == 0 && App.UserType == UserType.Excise && Container.Status==Status.Initiated)
            {
                stkAppprover.Visibility = Visibility.Visible;
            }
            else if (doc._status == 0 &&  App.UserType == UserType.Custom && Container.Status == Status.WeighingApproved)
            {
                stkAppprover.Visibility = Visibility.Visible;
                
            }
            else
            {
                stkAppprover.Visibility = Visibility.Collapsed;
            }
        }

        private async Task<bool> TrackShipment()
        {
            try
            {
                var trackingdetails =await APICall.TrackShipmentStatus(Container.Id);
                TrackingDetails.Clear();
                foreach (var detail in trackingdetails)
                {
             
                    TrackingDetails.Insert(0, detail);


                }
                Container._status = (int)trackingdetails.Last().Status;

            }
            catch
            {

            }
            SetStatus();
            return true;
        }

        private async Task<bool> TrackApprovals()
        {
            try
            {
                listDoc.DataContext = vm;
                listDoc.ItemsSource = vm.Approvalsdetail;
                    var detail =await APICall.TrackApproval(Container.Id);
                    switch (App.UserType)
                    {
                        case UserType.Excise:
                            if (vm.Approvalsdetail.Count() < 2)
                            {
                                vm.Approvalsdetail = new ObservableCollection<ApprovalsDetails>(detail.Where(x => x.DocId == 1 || x.DocId == 2).ToList());
                            }
                            break;
                        case UserType.Weighing:
                            if (vm.Approvalsdetail.Count() == 0)
                                vm.Approvalsdetail = new ObservableCollection<ApprovalsDetails>(detail.Where(x => x.DocId == 3).ToList());
                            break;
                        case UserType.Custom:
                            if (vm.Approvalsdetail.Count() == 0)
                                vm.Approvalsdetail = new ObservableCollection<ApprovalsDetails>(detail.Where(x => x.DocId == 4).ToList());
                            break;
                        default:
                            if (vm.Approvalsdetail.Count() < 4)
                            {
                                vm.Approvalsdetail = new ObservableCollection<ApprovalsDetails>(detail.ToList());
                            }
                            break;
                    }
                    //Approvalsdetail.Clear();
                    foreach (var s in detail)
                    {
                        if (vm.Approvalsdetail.Where(x => x.Document == s.Document).Count() > 0)
                            vm.Approvalsdetail.Where(x => x.Document == s.Document).First()._status = s._status;
                        //Approvalsdetail.Add(s.Event);


                    }

                if (detail.Where(c => c.Status == ApprovalStatus.Approved).Count() == 2 && Container.Status == Status.Initiated && App.UserType == UserType.Excise && !Approved)
                {
                    Approved = true;
                    Container._status = (int)Status.ExciseApproved;

                    ShipmentDetails shipmentRequest = new ShipmentDetails()
                    {
                        Id = Container.Id,
                        ContainerName = Container.ContainerName,
                        Weight = Convert.ToInt32(Container.Weight),
                        _status = 1,
                        Desc = String.Format("Documents for container {0} are approved successfully by {1}", Container.ContainerName, App.UserName),
                        Source = Container.Source,
                        Destination = Container.Destination
                    };
                    await APICall.AddShipmentStatus(shipmentRequest.Id, shipmentRequest);
                }
                if (detail.Where(c => c.Status == ApprovalStatus.Approved).Count() == 3 && Container.Status == Status.ExciseApproved && App.UserType == UserType.Weighing && !weighingApproved)
                {
                    prg.Visibility = Visibility.Visible;
                    weighingApproved = true;
                    VerifyWeight();
                }


                var doc = (ApprovalsDetails)listDoc.SelectedItem;
                //if (doc._status == 0 && App.UserType == UserType.Custom)
                //{
                //    stkAppprover.Visibility = Visibility.Visible;
                //}
                //else
                //{
                //    stkAppprover.Visibility = Visibility.Collapsed;
                //}
            }
            catch
            {

            }
            return true;
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            prg.Visibility = Visibility.Visible;
            btnSave.IsEnabled = false;
            btnClose.IsEnabled = false;
            selectedItem = (ApprovalsDetails)listDoc.SelectedItem;
            if (selectedItem != null)
            {
                if (selectedItem.DocId != 4)
                {
                    selectedItem.Id = Container.Id;
                    selectedItem._status = 1;
                    Container._status = (int)Status.Initiated;
                    SendForApprovals(1, (int)Status.Initiated);
                }
                else
                {
                    selectedItem.Id = Container.Id;
                    selectedItem._status = 1;
                    Container._status = (int)Status.CustomApproved;
                    SendForApprovals(1, (int)Status.CustomApproved);
                }
                stkAppprover.Visibility = Visibility.Collapsed;
            }
        }

        private  void btnCheckWeight_Click(object sender, RoutedEventArgs e)
        {
            stkWeight.Visibility = Visibility.Collapsed;
            prg.Visibility = Visibility.Visible;
            btnCheckWeight.IsEnabled = false;
            btnRejectWeighing.IsEnabled = false;
            selectedItem = (ApprovalsDetails)listDoc.SelectedItem;

            if (selectedItem != null)
            {
                selectedItem.Id = Container.Id;
                selectedItem._status = 1;
                Container._status = (int)Status.ExciseApproved;
                SendForApprovals(1, (int)Status.ExciseApproved);
            }
        }

        private async void VerifyWeight()
        {
            try
            {
                prg.Visibility = Visibility.Visible;
                ShipmentDetails shipmentRequest = new ShipmentDetails()
                {
                    Id = Container.Id,
                    ContainerName = Container.ContainerName,
                    Weight = Convert.ToInt32(txtWeight.Text),
                    Source = Container.Source,
                    Destination = Container.Destination
                };
                await APICall.VerifyWeight(shipmentRequest.Id, shipmentRequest);
                prg.Visibility = Visibility.Collapsed;
            }
            catch
            {

            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            prg.Visibility = Visibility.Visible;
            selectedItem = (ApprovalsDetails)listDoc.SelectedItem;
            if (selectedItem != null)
            {
                if (selectedItem.DocId != 4)
                {
                    selectedItem.Id = Container.Id;
                    selectedItem._status = 2;
                    Container._status = (int)Status.ExciseRejected;
                    SendForApprovals(2, (int)Status.ExciseRejected);
                    stkAppprover.Visibility = Visibility.Collapsed;
                }
                else
                {
                    selectedItem.Id = Container.Id;
                    selectedItem._status = 2;
                    Container._status = (int)Status.CustomRejected;
                    SendForApprovals(2, (int)Status.CustomRejected);
                    stkAppprover.Visibility = Visibility.Collapsed;
                }

            }
        }

        private async Task<bool> SendForApprovals(int docstatus,int containerstatus)
        {
            string desc=string.Empty;
            if(docstatus==1)
            {
                desc = String.Format("Document {0} is Approved by {1}",selectedItem.Document,txtUserName.Text);
            }
            else
            {
                 desc = String.Format("Document {0} is Rejected by {1}",selectedItem.Document,txtUserName.Text);
            }
            ApprovalsDetails approvalRequest = new ApprovalsDetails()
            {
                DocId = selectedItem.DocId,
                Document = selectedItem.Document,
                Id = selectedItem.Id,
                Url = selectedItem.Url,
                _status = docstatus,
                TransactionTime = DateTime.Now.ToString()
            };
            await APICall.UpdateApprovalStatus(approvalRequest.Id, approvalRequest);

            ShipmentDetails shipmentRequest = new ShipmentDetails()
            {
                Id = Container.Id,
                ContainerName = Container.ContainerName,
                Weight = Convert.ToInt32(Container.Weight),
                _status = containerstatus,
                Desc = desc,
                Source = Container.Source,
                Destination = Container.Destination
            };
            await APICall.AddShipmentStatus(shipmentRequest.Id, shipmentRequest);
        
            prg.Visibility = Visibility.Collapsed;
            btnSave.IsEnabled = true;
            btnClose.IsEnabled = true;

            return true;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Stop();
            App.mainWindow.AddChild(new ContainerListUserControl());
        }

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Stop();
            App.mainWindow.AddChild(new LoginUsercontrol());
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            prg.Visibility = Visibility.Visible;
            btnExport.IsEnabled = false;
            btnReject.IsEnabled = false;
            CalculatePayment();
            string desc = String.Format("Container {0} is Approved by {1} and gated in successfully", Container.ContainerName, txtUserName.Text, txtReason.Text);
            UpdateShipmentStatus(desc, (int)Status.GatedIn);
        }

        private  void btnReject_Click(object sender, RoutedEventArgs e)
        {
            prg.Visibility = Visibility.Visible;
            btnExport.IsEnabled = false;
            btnReject.IsEnabled = false;
            if(string.IsNullOrEmpty(txtReason.Text))
            {
                MessageBox.Show("Specify Reason");
                prg.Visibility = Visibility.Collapsed;
                btnExport.IsEnabled = true;
                btnReject.IsEnabled = true;
            }
            else
            {
                string desc = String.Format("Container {0} is Rejected by {1}\nReason:{2}", Container.ContainerName, txtUserName.Text, txtReason.Text);
                UpdateShipmentStatus(desc, (int)Status.Rejected);
               
            }
        }

        private async void UpdateShipmentStatus(string desc, int status)
        {
            ShipmentDetails shipmentRequest = new ShipmentDetails()
            {
                Id = Container.Id,
                ContainerName = Container.ContainerName,
                Weight = Convert.ToInt32(Container.Weight),
                _status = status,
                Desc = desc,
                Source = Container.Source,
                Destination = Container.Destination
            };
            await APICall.AddShipmentStatus(shipmentRequest.Id, shipmentRequest);
           
            prg.Visibility = Visibility.Collapsed;
        }

        private void btnRejectWeighing_Click(object sender, RoutedEventArgs e)
        {
            prg.Visibility = Visibility.Visible;
            stkWeight.Visibility = Visibility.Collapsed;
            btnCheckWeight.IsEnabled = false;
            btnRejectWeighing.IsEnabled = false;
            selectedItem = (ApprovalsDetails)listDoc.SelectedItem;

            if (selectedItem != null)
            {
                selectedItem.Id = Container.Id;
                selectedItem._status = 2;
                Container._status = (int)Status.WeighingRejected;
                SendForApprovals(2, (int)Status.WeighingRejected);
            }
        }
    }
}
