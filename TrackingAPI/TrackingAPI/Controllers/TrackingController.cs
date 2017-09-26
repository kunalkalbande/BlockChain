using Nethereum.RPC.Eth.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using TrackingAPI.Models;

namespace TrackingAPI.Controllers
{
    [RoutePrefix("api/v1/tracking")]
    public class TrackingController : ApiController
    {
        [HttpPost]
        [Route("{containerId}/shipmentDetail")]
        public async Task<IHttpActionResult> AddShipmentDetail(string containerId, [FromBody]ShipmentDetailRequest request)
        {
            try
            {
                return Ok(await Task.Run(async () =>
                {
                    var result =await Constants.UnlockAccount();
                    var initateShipment = Constants.shipmentContract.GetFunction("ShipmentDetail");
                    var transactionhash = await initateShipment.SendTransactionAsync(Constants.senderAddress, new Nethereum.Hex.HexTypes.HexBigInteger("0x3d0900"), null,request.ContainerId, Convert.ToInt32(request.Quantity), request.SupplierName, request.ContainerName);
                    var receipt = await Constants.MineAndGetReceiptAsync(Constants.web3, transactionhash);
                    return receipt;
                }));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("{containerId}/shipmentStatus")]
        public async Task<IHttpActionResult> AddShipmentStatus(string containerId, [FromBody]ShipmentRequest request)
        {
            try
            {
                return Ok(await Task.Run(async () =>
                {
                    var result =await Constants.UnlockAccount();
                    var ShipmentStatus = Constants.shipmentContract.GetFunction("ShipmentStatus");
                    await Constants.web3.Miner.Start.SendRequestAsync();
                    var transactionhash = await ShipmentStatus.SendTransactionAsync(Constants.senderAddress, new Nethereum.Hex.HexTypes.HexBigInteger("0x3d0900"), null, request.Id, Convert.ToInt32(request.Weight), DateTime.Now.ToString(), request.Desc, request.Status,Constants.trackingaddress, request.ContainerName, request.Source, request.Destination);
                    var receipt = await Constants.MineAndGetReceiptAsync(Constants.web3, transactionhash);
                    return receipt;
                }));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("{containerId}/approvalStatus")]
        public async Task<IHttpActionResult> UpdateApprovalStatus(string containerId, [FromBody]ApprovalRequest request)
        {
            try
            {
                return Ok(await Task.Run(async () =>
                {
                    var result = await Constants.UnlockAccount();
                    var UpdateApprovalStatus = Constants.approvalsContract.GetFunction("UpdateApprovalStatus");
                    var transactionhash = await UpdateApprovalStatus.SendTransactionAsync(Constants.senderAddress, new Nethereum.Hex.HexTypes.HexBigInteger("0x3d0900"), null, request.Id, request.Document, request._status, DateTime.Now.ToString(), request.Url, request.DocId);
                    var receipt = await Constants.MineAndGetReceiptAsync(Constants.web3, transactionhash);
                    return receipt;
                }));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("{containerId}/verifyWeight")]
        public async Task<IHttpActionResult> VerifyWeight(string containerId, [FromBody]WeighingRequest request)
        {
            try
            {
                return Ok(await Task.Run(async () =>
                {
                    var result = await Constants.UnlockAccount();
                    var VerifyWeight = Constants.weighingContract.GetFunction("VerifyWeight");
                    var transactionhash = await VerifyWeight.SendTransactionAsync(Constants.senderAddress, new Nethereum.Hex.HexTypes.HexBigInteger("0x3d0900"), null, containerId, Convert.ToInt32(request.Weight), DateTime.Now.ToString(), Constants.shipmentaddress, Constants.trackingaddress, request.ContainerName, request.Source, request.Destination);
                    var receipt = await Constants.MineAndGetReceiptAsync(Constants.web3, transactionhash);
                    return receipt;
                }));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpPost]
        [Route("{containerId}/calculatePayment")]
        public async Task<IHttpActionResult> CalculatePayment(string containerId, [FromBody]PaymentRequest request)
        {
            try
            {
                return Ok(await Task.Run(async () =>
                {
                    var result = await Constants.UnlockAccount();
                    var calculatePayments = Constants.paymentContract.GetFunction("CalculatePayments");
                    var transactionhash = await calculatePayments.SendTransactionAsync(Constants.senderAddress, new Nethereum.Hex.HexTypes.HexBigInteger("0x3d0900"), null, request.Unit, request.SupplierName, containerId, request.Unit, request.ContainerName, DateTime.Now.ToString());
                    var receipt = await Constants.MineAndGetReceiptAsync(Constants.web3, transactionhash);
                    return receipt;
                }));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpPost]
        [Route("{supplierName}/mapSupplierBanks")]
        public async Task<IHttpActionResult> MapSupplierBanks(string supplierName)
        {
            try
            {
                return Ok(await Task.Run(async () =>
                {
                    var result = await Constants.UnlockAccount();
                    var supplierBanks = Constants.paymentContract.GetFunction("MapSupplierBanks");
                    var transactionhash = await supplierBanks.SendTransactionAsync(Constants.senderAddress, new Nethereum.Hex.HexTypes.HexBigInteger("0x3d0900"), null, supplierName);
                    var receipt = await Constants.MineAndGetReceiptAsync(Constants.web3, transactionhash);
                    return receipt;
                }));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpGet]
        [Route("trackShipments")]
        public async Task<IHttpActionResult> TrackShipmentsStatus()
        {
            try
            {
                return base.Ok(await Task.Run(async () =>
                {
                    List<ShipmentRequest> details = new List<ShipmentRequest>();
                    var result = await Constants.UnlockAccount();
                    var trackshipmentevent = Constants.shipmentContract.GetEvent("TrackShipmentStatus");
                    var fi = await trackshipmentevent.CreateFilterAsync(new BlockParameter(0));
                    var trackingshipmentdetails = await trackshipmentevent.GetAllChanges<ShipmentRequest>(fi);

                    if (trackingshipmentdetails != null && trackingshipmentdetails.Count > 0)
                    {
                        details = trackingshipmentdetails.OrderByDescending(x => Convert.ToDateTime(x.Event.TransactionTime)).GroupBy(x => x.Event.Id).Select(l => l.First().Event).ToList();
                    }
                    return details;

                }));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpGet]
        [Route("{containerId}/trackShipment")]
        public async Task<IHttpActionResult> TrackShipmentStatus(string containerId)
        {
            try
            {
                return base.Ok(await Task.Run(async () =>
                {                  
                    var result = await Constants.UnlockAccount();
                    var trackevent = Constants.trackingContract.GetEvent("TrackShipment");
                    var fi = await trackevent.CreateFilterAsync(new BlockParameter(0));
                    var trackingdetails = await trackevent.GetAllChanges<TrackingDetails>(fi);
                    var details = trackingdetails.Where(x => x.Event.Id == containerId).Select(x=>x.Event).ToList();
                    return details;
                }));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("{containerId}/trackApproval")]
        public async Task<IHttpActionResult> TrackApproval(string containerId)
        {
            try
            {
                return base.Ok(await Task.Run(async () =>
                {
                    List<ApprovalRequest> detail = new List<ApprovalRequest>();
                    var result = await Constants.UnlockAccount();
                    var trackapprovalsevent = Constants.approvalsContract.GetEvent("docstatus");
                    var fi = await trackapprovalsevent.CreateFilterAsync(new BlockParameter(0));
                    var trackingapprovalsdetails = await trackapprovalsevent.GetAllChanges<ApprovalRequest>(fi);
                    if(trackingapprovalsdetails.Count()>0)
                    {
                         detail = trackingapprovalsdetails.ToList().Where(x => x.Event.Id == containerId).OrderByDescending(x => Convert.ToDateTime(x.Event.TransactionTime)).GroupBy(x => x.Event.Document).Select(x => x.First()).OrderBy(x => x.Event.DocId).Select(x=>x.Event).ToList();
                        return detail;
                    }
                    return detail;
                }));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("{supplierName}/getPaymentDetails")]
        public async Task<IHttpActionResult> GetPaymentDetails(string supplierName)
        {
            try
            {
                return base.Ok(await Task.Run(async () =>
                {
                    var result = await Constants.UnlockAccount();
                    var paymentEvent = Constants.paymentContract.GetEvent("Pay");
                    var fi = await paymentEvent.CreateFilterAsync(new BlockParameter(0));
                    var paymentDetails = await paymentEvent.GetAllChanges<PaymentRequest>(fi);
                    return paymentDetails.ToList().Select(p => p.Event).Where(d => d.SupplierName == supplierName).ToList();
                }));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("{supplierName}/shipmentDetail")]
        public async Task<IHttpActionResult> GetShipmentDetail(string supplierName)
        {
            try
            {
                return base.Ok(await Task.Run(async () =>
                {
                    var result = await Constants.UnlockAccount();
                    var shipmentdetail =Constants.shipmentContract.GetEvent("TrackShipmentDetail");

                    var filterInput = await shipmentdetail.CreateFilterAsync(new BlockParameter(0));
                    var shipmentDetails = await shipmentdetail.GetAllChanges<ShipmentDetailRequest>(filterInput);
                    return shipmentDetails.Where(s => s.Event.SupplierName == supplierName).Select(s=>s.Event).ToList();
                }));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("{containerId}/containerShipmentDetail")]
        public async Task<IHttpActionResult> GetContainerShipmentDetail(string containerId)
        {
            try
            {
                return base.Ok(await Task.Run(async () =>
                {
                    var result = await Constants.UnlockAccount();
                    var shipmentdetail = Constants.shipmentContract.GetEvent("TrackShipmentDetail");

                    var filterInput = await shipmentdetail.CreateFilterAsync(new BlockParameter(0));
                    var shipmentDetails = await shipmentdetail.GetAllChanges<ShipmentDetailRequest>(filterInput);
                    return shipmentDetails.Where(s => s.Event.ContainerId == containerId && !string.IsNullOrEmpty (s.Event.SupplierName)).Select(s=>s.Event).ToList();
                }));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("{containerId}/containerStatus")]
        public async Task<IHttpActionResult> GetContainerStatus(string containerId)
        {
            try
            {
                return base.Ok(await Task.Run(async () =>
                {
                    var result = await Constants.UnlockAccount();
                    var Supplierfunc = Constants.shipmentContract.GetFunction("CStatus");
                    return (await Supplierfunc.CallAsync<int>(containerId));
                }));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("{containerId}/containerTimespan")]
        public async Task<IHttpActionResult> GetContainerTimespan(string containerId)
        {
            try
            {
                return base.Ok(await Task.Run(async () =>
                {
                    var result = await Constants.UnlockAccount();
                    var Supplierfunc = Constants.shipmentContract.GetFunction("Time");
                    return (await Supplierfunc.CallAsync<string>(containerId));
                }));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("{containerId}/containerUnit")]
        public async Task<IHttpActionResult> GetContainerUnit(string containerId)
        {
            try
            {
                return base.Ok(await Task.Run(async () =>
                {
                    var result = await Constants.UnlockAccount();
                    var Supplierfunc = Constants.shipmentContract.GetFunction("Unit");
                    return (await Supplierfunc.CallAsync<int>(containerId));
                }));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("{containerId}/containerSupplier")]
        public async Task<IHttpActionResult> GetContainerSupplier(string containerId)
        {
            try
            {
                return base.Ok(await Task.Run(async () =>
                {
                    var result = await Constants.UnlockAccount();
                    var Supplierfunc = Constants.shipmentContract.GetFunction("Supplier");
                    return (await Supplierfunc.CallAsync<string>(containerId));
                }));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
