using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.RPC.Eth.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFTrackingBC.Models
{
    public class Payment
    {
        public static async Task MapSupplierBanks(string SupplierName)
        {
            try
            {
                var supplierBanks = App.mainWindow.paymentContract.GetFunction("MapSupplierBanks");
                var transactionhash = await supplierBanks.SendTransactionAsync(App.mainWindow.senderAddress, new Nethereum.Hex.HexTypes.HexBigInteger("0x3d0900"), null, SupplierName);
                var receipt = await App.mainWindow.MineAndGetReceiptAsync(App.mainWindow.web3, transactionhash);
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        
        public static async Task CalculatePayments(int quantity, string supplierName, string containerId, int units,string containerName,string time)
        {
            try
            {
                var calculatePayments = App.mainWindow.paymentContract.GetFunction("CalculatePayments");
                var transactionhash = await calculatePayments.SendTransactionAsync(App.mainWindow.senderAddress, new Nethereum.Hex.HexTypes.HexBigInteger("0x3d0900"), null, quantity, supplierName, containerId, units,containerName,time);
                var receipt = await App.mainWindow.MineAndGetReceiptAsync(App.mainWindow.web3, transactionhash);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public static async Task<List<PaymentDetails>> GetPaymentDetails()
        {
            try
            {
                var filterInput = new Nethereum.RPC.Eth.Filters.NewFilterInput();
                filterInput.FromBlock = new BlockParameter(0);
                filterInput.Address = new string[] { App.mainWindow.paymentaddress };
                var paymentEvent = App.mainWindow.paymentContract.GetEvent("Pay");
                var paymentDetails = await paymentEvent.GetAllChanges<PaymentDetails>(filterInput);
                return paymentDetails.ToList().Select(p=>p.Event).ToList();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }

  
}
