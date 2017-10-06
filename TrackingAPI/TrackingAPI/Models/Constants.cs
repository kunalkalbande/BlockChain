using Nethereum.RPC.Eth.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace TrackingAPI.Models
{
    public class Constants
    {
        static Nethereum.JsonRpc.Client.IClient client = new Nethereum.JsonRpc.Client.RpcClient(new Uri("http://synbckaam.southcentralus.cloudapp.azure.com:8545"));
        public static Nethereum.Web3.Web3 web3 = new Nethereum.Web3.Web3();
        public const string senderAddress =// "0x2d093225389fbd83a32150b46efd35665aeea7d3";
        "0x12890d2cce102216644c59dae5baed380d84830c";
        public const string password = //"Pulsar6419!!";
        "password";
        public const string trackingaddress = "0x243e72b69141f6af525a9a5fd939668ee9f2b354";
        public const string shipmentaddress = "0x2a212f50a2a020010ea88cc33fc4c333e6a5c5c4";
        public const string approvalsaddress = "0xd0828aeb00e4db6813e2f330318ef94d2bba2f60";
        public const string weighingaddress = "0x6c498f0f83d0bbec758ee7f23e13c9ee522a4c8f";
        public const string paymentaddress = "0x786a30e1ab0c58303c85419b9077657ad4fdb0ea";
        static string shipmentabi  = @"[{""constant"":false,""inputs"":[{""name"":""containerId"",""type"":""address""},{""name"":""weight"",""type"":""int256""},{""name"":""date"",""type"":""string""},{""name"":""desc"",""type"":""string""},{""name"":""status"",""type"":""int256""},{""name"":""trackingaddress"",""type"":""address""},{""name"":""name"",""type"":""string""},{""name"":""source"",""type"":""string""},{""name"":""destination"",""type"":""string""}],""name"":""ShipmentStatus"",""outputs"":[],""type"":""function""},{""constant"":true,""inputs"":[{""name"":"""",""type"":""address""}],""name"":""Weight"",""outputs"":[{""name"":"""",""type"":""int256""}],""type"":""function""},{""constant"":true,""inputs"":[{""name"":"""",""type"":""address""}],""name"":""Supplier"",""outputs"":[{""name"":"""",""type"":""string""}],""type"":""function""},{""constant"":false,""inputs"":[{""name"":""containerId"",""type"":""address""},{""name"":""unit"",""type"":""int256""},{""name"":""supplier"",""type"":""string""},{""name"":""containerName"",""type"":""string""}],""name"":""ShipmentDetail"",""outputs"":[],""type"":""function""},{""constant"":true,""inputs"":[{""name"":"""",""type"":""address""}],""name"":""Unit"",""outputs"":[{""name"":"""",""type"":""int256""}],""type"":""function""},{""constant"":true,""inputs"":[{""name"":"""",""type"":""address""}],""name"":""CStatus"",""outputs"":[{""name"":"""",""type"":""int256""}],""type"":""function""},{""constant"":true,""inputs"":[{""name"":"""",""type"":""address""}],""name"":""Time"",""outputs"":[{""name"":"""",""type"":""string""}],""type"":""function""},{""inputs"":[],""type"":""constructor""},{""anonymous"":false,""inputs"":[{""indexed"":false,""name"":""containerId"",""type"":""address""},{""indexed"":false,""name"":""date"",""type"":""string""},{""indexed"":false,""name"":""status"",""type"":""int256""},{""indexed"":false,""name"":""name"",""type"":""string""},{""indexed"":false,""name"":""source"",""type"":""string""},{""indexed"":false,""name"":""destination"",""type"":""string""},{""indexed"":false,""name"":""weight"",""type"":""int256""}],""name"":""TrackShipmentStatus"",""type"":""event""},{""anonymous"":false,""inputs"":[{""indexed"":false,""name"":""containerId"",""type"":""address""},{""indexed"":false,""name"":""unit"",""type"":""int256""},{""indexed"":false,""name"":""supplier"",""type"":""string""},{""indexed"":false,""name"":""containerName"",""type"":""string""}],""name"":""TrackShipmentDetail"",""type"":""event""}]";
        static string paymentabi   = @"[{""constant"":false,""inputs"":[],""name"":""MapSupplierIds"",""outputs"":[],""type"":""function""},{""constant"":false,""inputs"":[{""name"":""qty"",""type"":""int256""},{""name"":""supplierName"",""type"":""string""},{""name"":""containerId"",""type"":""address""},{""name"":""units"",""type"":""int256""},{""name"":""containerName"",""type"":""string""},{""name"":""time"",""type"":""string""}],""name"":""CalculatePayments"",""outputs"":[],""type"":""function""},{""constant"":false,""inputs"":[{""name"":""supplierName"",""type"":""string""}],""name"":""MapSupplierBanks"",""outputs"":[],""type"":""function""},{""constant"":true,""inputs"":[{""name"":"""",""type"":""address""}],""name"":""Bank"",""outputs"":[{""name"":"""",""type"":""string""}],""type"":""function""},{""inputs"":[],""type"":""constructor""},{""anonymous"":false,""inputs"":[{""indexed"":false,""name"":""qty"",""type"":""int256""},{""indexed"":false,""name"":""supplierName"",""type"":""string""},{""indexed"":false,""name"":""containerId"",""type"":""address""},{""indexed"":false,""name"":""bank"",""type"":""string""},{""indexed"":false,""name"":""units"",""type"":""int256""},{""indexed"":false,""name"":""containerName"",""type"":""string""},{""indexed"":false,""name"":""time"",""type"":""string""}],""name"":""Pay"",""type"":""event""}]";
        static string trackingabi  = @"[{""constant"":false,""inputs"":[{""name"":""containerId"",""type"":""address""},{""name"":""weight"",""type"":""int256""},{""name"":""desc"",""type"":""string""},{""name"":""date"",""type"":""string""},{""name"":""status"",""type"":""int256""},{""name"":""name"",""type"":""string""},{""name"":""source"",""type"":""string""},{""name"":""destination"",""type"":""string""}],""name"":""ShipmentTracking"",""outputs"":[],""type"":""function""},{""inputs"":[],""type"":""constructor""},{""anonymous"":false,""inputs"":[{""indexed"":false,""name"":""containerId"",""type"":""address""},{""indexed"":false,""name"":""weight"",""type"":""int256""},{""indexed"":false,""name"":""desc"",""type"":""string""},{""indexed"":false,""name"":""date"",""type"":""string""},{""indexed"":false,""name"":""status"",""type"":""int256""},{""indexed"":false,""name"":""name"",""type"":""string""},{""indexed"":false,""name"":""source"",""type"":""string""},{""indexed"":false,""name"":""destination"",""type"":""string""}],""name"":""TrackShipment"",""type"":""event""}]";
        static string approvalsabi = @"[{""constant"":false,""inputs"":[{""name"":""containerId"",""type"":""address""},{""name"":""doc"",""type"":""string""},{""name"":""status"",""type"":""int256""},{""name"":""date"",""type"":""string""},{""name"":""docurl"",""type"":""string""},{""name"":""docid"",""type"":""int256""}],""name"":""UpdateApprovalStatus"",""outputs"":[],""type"":""function""},{""inputs"":[],""type"":""constructor""},{""anonymous"":false,""inputs"":[{""indexed"":false,""name"":""containerId"",""type"":""address""},{""indexed"":false,""name"":""doc"",""type"":""string""},{""indexed"":false,""name"":""status"",""type"":""int256""},{""indexed"":false,""name"":""date"",""type"":""string""},{""indexed"":false,""name"":""docurl"",""type"":""string""},{""indexed"":false,""name"":""docid"",""type"":""int256""}],""name"":""docstatus"",""type"":""event""}]";
        static string weighingabi  = @"[{""constant"":false,""inputs"":[{""name"":""containerId"",""type"":""address""},{""name"":""weight"",""type"":""int256""},{""name"":""date"",""type"":""string""},{""name"":""shipmentaddress"",""type"":""address""},{""name"":""trackingaddress"",""type"":""address""},{""name"":""name"",""type"":""string""},{""name"":""source"",""type"":""string""},{""name"":""destination"",""type"":""string""}],""name"":""VerifyWeight"",""outputs"":[],""type"":""function""},{""inputs"":[],""type"":""constructor""}]";
        public static Nethereum.Web3.Contract paymentContract   = web3.Eth.GetContract(paymentabi, paymentaddress);
        public static Nethereum.Web3.Contract trackingContract  = web3.Eth.GetContract(trackingabi, trackingaddress);
        public static Nethereum.Web3.Contract shipmentContract  = web3.Eth.GetContract(shipmentabi, shipmentaddress);
        public static Nethereum.Web3.Contract approvalsContract = web3.Eth.GetContract(approvalsabi, approvalsaddress);
        public static Nethereum.Web3.Contract weighingContract  = web3.Eth.GetContract(weighingabi, weighingaddress);
        public static async Task<bool> UnlockAccount()
        {
            
            var UnblockAccountRes = await web3.Personal.UnlockAccount.SendRequestAsync(senderAddress, password, 600);//(senderAddress, password, new HexBigInteger(120),new object[] { });       
            return UnblockAccountRes;
        }

        public static async Task<TransactionReceipt> MineAndGetReceiptAsync(Nethereum.Web3.Web3 web3, string transactionHash)
        {
            var result = await web3.Miner.SetGasPrice.SendRequestAsync(new Nethereum.Hex.HexTypes.HexBigInteger("0x3d0900"));
            var miningResult = await web3.Miner.Start.SendRequestAsync();
            var receipt = await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transactionHash);

            while (receipt == null)
            {
                Thread.Sleep(1000);
                receipt = await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transactionHash);
            }

            miningResult = await web3.Miner.Stop.SendRequestAsync();

            return receipt;
        }
    }
}