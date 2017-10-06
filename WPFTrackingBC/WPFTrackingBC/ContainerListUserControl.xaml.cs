using Nethereum.RPC.Eth.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
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
    /// Interaction logic for ContainerListUserControl.xaml
    /// </summary>
    public partial class ContainerListUserControl : UserControl
    {
        ShipmentDetailViewModel vm = new ShipmentDetailViewModel();
        ContainerDetails detail = new ContainerDetails();
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        
        public ContainerListUserControl()
        {
            InitializeComponent();
            this.Loaded += ContainerListUserControl_Loaded;
        }

        private async void ContainerListUserControl_Loaded(object sender, RoutedEventArgs e)
        {

            
            if (App.UserType == UserType.Initiater)
            {
                btnadd.Visibility = Visibility.Visible;
            }
            else
            {
                btnNotification.Visibility = Visibility.Visible;
            }
            {
                vm.ContainerList = new ObservableCollection<ShipmentDetails>();
            }
            prg.Visibility = Visibility.Visible;
        
            await TrackShipmentStatus();
            prg.Visibility = Visibility.Collapsed;
            this.DataContext = vm;
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 10);
            dispatcherTimer.Start();
        }

        //private void TrackShipmentStatus1()
        //{
        //    lock (o)
        //    {
        //        TrackShipmentStatus();
        //    }

        //}

        private void TriggerNotification()
        {
            
            switch(App.UserType)
            {
                case UserType.Excise:
                    vm.NotificationList = new ObservableCollection<ShipmentDetails>(vm.ContainerList.Where(x => x.Status == Status.Initiated).ToList());
                    txtNotification.Text = vm.NotificationList.Count.ToString();
                    break;
                case UserType.VGM:
                    vm.NotificationList = new ObservableCollection<ShipmentDetails>(vm.ContainerList.Where(x => x.Status == Status.ExciseApproved).ToList());
                    txtNotification.Text = vm.NotificationList.Count.ToString();
                    break;
                case UserType.Custom:
                    vm.NotificationList = new ObservableCollection<ShipmentDetails>(vm.ContainerList.Where(x => x.Status == Status.WeighingApproved).ToList());
                    txtNotification.Text = vm.NotificationList.Count.ToString();
                    break;
                case UserType.ExportAuthority:
                    vm.NotificationList = new ObservableCollection<ShipmentDetails>(vm.ContainerList.Where(x => x.Status == Status.CustomApproved).ToList());
                    txtNotification.Text = vm.NotificationList.Count.ToString();
                    break;

            }
        }
        object o = new object();
        private  void dispatcherTimer_Tick(object sender, EventArgs e)
        {
          
               TrackShipmentStatus();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            detail = new ContainerDetails();
            detail.btnSave.Click += BtnSave_Click;
            detail.Show();
        }

        private async void BtnSave_Click(object sender, RoutedEventArgs e)
        {

            prg.Visibility = Visibility.Visible;
            detail.Hide();
            await InitateShipment();

            //TrackingUserControl tracking = new TrackingUserControl();
            //tracking.Container = ((ShipmentDetails)listContainer.Items[0]);
            //App.mainWindow.AddChild(tracking);
            prg.Visibility = Visibility.Collapsed;

        }

        private async Task<bool> InitateShipment()
        {
            string url1 = "http://idtp376/Pdf/GST_B2B_INVOICE_CASES-CF.pdf";
            string url2 = "http://idtp376/Pdf/Ethereum%20Multi-Member%20Consortium%20Network.pdf";
            string url3 = "http://idtp376/Pdf/ASF-VGM-Declaration-Form.pdf";
            string url4 = "http://idtp376/Pdf/Ethereum%20Consortium%20Blockchain%20in%20Azure%20Marketplace.pdf";

            ShipmentDetailForPayment shipmentDetailrequest = new ShipmentDetailForPayment()
            {
                ContainerId = detail.txtContainerId.Text,
                ContainerName = detail.txtContainerName.Text,
                Quantity = Convert.ToInt32(detail.txtQuantity.Text),
                SupplierName = string.Empty
            };
           await APICall.AddShipmentDetail(shipmentDetailrequest.ContainerId, shipmentDetailrequest);

            ShipmentDetails shipmentRequest = new ShipmentDetails()
            {
                Id=detail.txtContainerId.Text,
                ContainerName=detail.txtContainerName.Text,
                Weight=Convert.ToInt32(detail.txtContainerWeight.Text),
                _status=9,
                Desc = string.Format("Container {0} is initiated successfully at {1} from {2} to {3}", detail.txtContainerName.Text, DateTime.Now.ToString(), detail.txtSource.Text, detail.txtDestination.Text),
                Source=detail.txtSource.Text,
                Destination=detail.txtDestination.Text
            };
            await APICall.AddShipmentStatus(shipmentRequest.Id, shipmentRequest);
            ApprovalsDetails approvalRequest = new ApprovalsDetails()
            {
                DocId = 1,
                Document = "Excise Form",
                Id = detail.txtContainerId.Text,
                Url = url1,
                _status = 0,
                TransactionTime = DateTime.Now.ToString()
            };
            await APICall.UpdateApprovalStatus(approvalRequest.Id, approvalRequest);
            shipmentRequest.Desc = string.Format("Excise Form for Container {0} is uploaded successfully at {1}", detail.txtContainerName.Text, DateTime.Now.ToString());
            await APICall.AddShipmentStatus(shipmentRequest.Id, shipmentRequest);

            approvalRequest = new ApprovalsDetails()
            {
                DocId = 2,
                Document = "Packaging List",
                Id = detail.txtContainerId.Text,
                Url = url2,
                _status = 0,
                TransactionTime = DateTime.Now.ToString()
            };
            await APICall.UpdateApprovalStatus(approvalRequest.Id, approvalRequest);
            shipmentRequest.Desc = string.Format("Packaging List for Container {0} is uploaded successfully at {1}", detail.txtContainerName.Text, DateTime.Now.ToString());
            await APICall.AddShipmentStatus(shipmentRequest.Id, shipmentRequest);

            approvalRequest = new ApprovalsDetails()
            {
                DocId = 3,
                Document = "VGM",
                Id = detail.txtContainerId.Text,
                Url = url3,
                _status = 0,
                TransactionTime = DateTime.Now.ToString()
            };
            await APICall.UpdateApprovalStatus(approvalRequest.Id, approvalRequest);
            shipmentRequest.Desc = string.Format("VGM for Container {0} is uploaded successfully at {1}", detail.txtContainerName.Text, DateTime.Now.ToString());
            await APICall.AddShipmentStatus(shipmentRequest.Id, shipmentRequest);

            approvalRequest = new ApprovalsDetails()
            {
                DocId = 4,
                Document = "Custom Form",
                Id = detail.txtContainerId.Text,
                Url = url4,
                _status = 0,
                TransactionTime = DateTime.Now.ToString()
            };
            await APICall.UpdateApprovalStatus(approvalRequest.Id, approvalRequest);
            shipmentRequest.Desc = string.Format("Custom Form for Container {0} is uploaded successfully at {1}", detail.txtContainerName.Text, DateTime.Now.ToString());
            shipmentRequest._status = 11;
            await APICall.AddShipmentStatus(shipmentRequest.Id, shipmentRequest);
           
            return true;
        }

        private async Task<bool> TrackShipmentStatus()
        {
          
            try
            {
                var trackingshipmentdetails =await APICall.TrackShipmentsStatus();


                if (trackingshipmentdetails != null && trackingshipmentdetails.Count > 0)
                {
                    
                    foreach (var detail in trackingshipmentdetails)
                    {
                        
                        if (vm.ContainerList.Where(l => l.Id == detail.Id).Count() == 0)
                        {
                            vm.ContainerList.Insert(0, new ShipmentDetails()
                            {
                                Id = detail.Id,
                                ContainerName = detail.ContainerName,
                                _status = (int)detail.Status,
                                IsVisible = Visibility.Collapsed,
                                Weight = Convert.ToInt32(detail.Weight),
                                Source = detail.Source,
                                Destination = detail.Destination,
                                TransactionTime = detail.TransactionTime
                            });
                        }
                        else
                        {
                            vm.ContainerList.Where(l => l.Id == detail.Id).First()._status = detail._status;
                        }
                    }

                }

                TriggerNotification();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        private void listContainer_Selected(object sender, SelectionChangedEventArgs e)
        {
            if (listContainer.SelectedIndex >= 0)
            {
                dispatcherTimer.Stop();

                TrackingUserControl tracking = new TrackingUserControl();
                tracking.Container = ((ShipmentDetails)listContainer.SelectedItem);
                if (!string.IsNullOrEmpty(tracking.Container.Id))
                {
                    App.mainWindow.AddChild(tracking);
                }
                else
                {
                    listContainer.SelectedIndex = -1;
                }
            }
        }

        private void btnNotification_Click(object sender, RoutedEventArgs e)
        {
            if(vm.NotificationList!=null && vm.NotificationList.Count()>0)
            popupContainer.IsOpen = true;
        }

        private void listNotification_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listNotification.SelectedIndex >= 0)
            {
                dispatcherTimer.Stop();

                TrackingUserControl tracking = new TrackingUserControl();
                tracking.Container = ((ShipmentDetails)listNotification.SelectedItem);
                if (!string.IsNullOrEmpty(tracking.Container.Id))
                {
                    App.mainWindow.AddChild(tracking);
                    dispatcherTimer.Stop();
                }
                else
                {
                    listContainer.SelectedIndex = -1;
                }
                
            }
        }
    }
}
