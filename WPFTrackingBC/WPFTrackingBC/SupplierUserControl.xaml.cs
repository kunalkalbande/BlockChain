﻿using Nethereum.RPC.Eth.DTOs;
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
    /// Interaction logic for SupplierUserControl.xaml
    /// </summary>
    public partial class SupplierUserControl : UserControl
    {
        SupplierViewModel vm = new SupplierViewModel();
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        ObservableCollection<PaymentDetails> Details = new ObservableCollection<PaymentDetails>();
        public SupplierUserControl()
        {
            InitializeComponent();
            this.Loaded += SupplierUserControl_Loaded;
        }
        int Amount = 0;
        private async void SupplierUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            prg.Visibility = Visibility.Visible;
            prg1.Visibility = Visibility.Visible;
            this.DataContext = vm;
            vm.Supplier = App.UserName;
            vm.ContainerList = new ObservableCollection<ShipmentDetails>();
            vm.PaymentDetails = new ObservableCollection<PaymentDetails>();
            vm.Bank = "N/A";
            await GetTransactionDetails();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 10);
            dispatcherTimer.Start();
            prg.Visibility = Visibility.Collapsed;
            prg1.Visibility = Visibility.Collapsed;
        }

        private async void dispatcherTimer_Tick(object sender, EventArgs e)
        {
           await GetTransactionDetails();
        }

        private async Task<bool> GetTransactionDetails()
        {
           
               var details = new ObservableCollection<PaymentDetails>( (await APICall.GetPaymentDetails(App.UserName)).GroupBy(d => d.ContainerId).Select(c => c.First()).ToList());
          

            //vm.PaymentDetails.ToList().AddRange((_paymentdetail).Where(x => !vm.PaymentDetails.Select(y => y.ContainerId).Contains(x.ContainerId)).Select(x => x).ToList());
            var supplierShipment = (await APICall.GetShipmentDetail(App.UserName)).Where(x=> !details.Select(y=>y.ContainerId).Contains(x.ContainerId)).Select(x=>x).ToList() ;
            vm.Balance = 0;
            
            details.ToList().ForEach(x => { vm.Balance += x.Quantity; vm.Bank = x.Bank; if (x.Quantity > 0) x.Status = Status.GatedIn; });
            supplierShipment.ForEach(x => details.Add(new PaymentDetails() {ContainerId=x.ContainerId, ContainerName = x.ContainerName, Quantity = 0, Unit = x.Quantity }));
            await UpdateStatus(details.ToList());
            vm.PaymentDetails = details;
            await TrackShipmentStatus();

            return true;
        }
        private async Task<bool> TrackShipmentStatus()
        {

            try
            {
                var trackingshipmentdetails = await APICall.TrackShipmentsStatus();


                if (trackingshipmentdetails != null && trackingshipmentdetails.Count > 0)
                {

                    foreach (var detail in trackingshipmentdetails)
                    {
                        var Supplier = await APICall.GetContainerSupplier(detail.Id);

                        if (vm.ContainerList.Where(l => l.Id == detail.Id).Count() == 0)
                        {
                            
                            var unit = await APICall.GetContainerUnit(detail.Id);
                            vm.ContainerList.Insert(0, new ShipmentDetails()
                            {
                                Id = detail.Id,
                                ContainerName = detail.ContainerName,
                                _status = (int)detail.Status,
                                IsVisible = Visibility.Collapsed,
                                Weight = Convert.ToInt32(detail.Weight),
                                Source = detail.Source,
                                Destination = detail.Destination,
                                TransactionTime = detail.TransactionTime,
                                Unit=unit,
                                Supplier=Supplier
                            });

                        }
                        else
                        {
                            vm.ContainerList.Where(l => l.Id == detail.Id).First()._status = detail._status;
                            vm.ContainerList.Where(l => l.Id == detail.Id).First().Supplier = Supplier;
                        }
                    }

                }

             
                return true;
            }
            catch
            {
                return false;
            }
        }
        private async Task UpdateStatus(List<PaymentDetails> contaiinerList)
        {
          
            foreach (var container in contaiinerList)
            {
                container.Status= (Status)(await APICall.GetContainerStatus(container.ContainerId));
                container.TransactionTime= (await APICall.GetContainerTimespan(container.ContainerId));
            }
        }

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            App.mainWindow.AddChild(new LoginUsercontrol());
            dispatcherTimer.Stop();
        }

        private async void Approve_Click(object sender, RoutedEventArgs e)
        {
            prg1.Visibility = Visibility.Visible;
            ((Button)sender).IsEnabled = false;
            var data = ((Button)sender).DataContext as ShipmentDetails;
            ShipmentDetailForPayment shipmentDetailrequest = new ShipmentDetailForPayment()
            {
                ContainerId = data.Id,
                ContainerName =data.ContainerName,
                Quantity = Convert.ToInt32(data.Unit),
                SupplierName = App.UserName
            };
            await APICall.AddShipmentDetail(shipmentDetailrequest.ContainerId, shipmentDetailrequest);

            ShipmentDetails shipmentRequest = new ShipmentDetails()
            {
                Id = data.Id,
                ContainerName = data.ContainerName,
                Weight = Convert.ToInt32(data.Weight),
                _status = 0,
                Desc = string.Format("Container {0} is Approved by {1}",data.ContainerName,App.UserName),
                Source = data.Source,
                Destination = data.Destination
            };
            await APICall.AddShipmentStatus(shipmentRequest.Id, shipmentRequest);
            prg.Visibility = Visibility.Collapsed;
            prg1.Visibility = Visibility.Collapsed;
        }
    }
}
