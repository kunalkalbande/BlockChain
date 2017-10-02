using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WPFTrackingBC.Models;
namespace WPFTrackingBC
{
    public class APICall
    {
        const string API = "http://idtp376/TrackingAPI/api/v1/tracking/";
        public static async Task<bool> AddShipmentDetail(string containerId, ShipmentDetailForPayment request)
        {
            bool result=false;
            var jsonstring = JsonConvert.SerializeObject(request);
            string url = String.Format(API + "{0}/shipmentDetail",containerId);
            System.Net.HttpWebRequest requestForJason = (HttpWebRequest)WebRequest.Create(url);
            requestForJason.Method = "POST"; //make an HTTP POST
                                             //  var jsonstring = WebUtility.UrlEncode(JsonConvert.SerializeObject(vehiclebookingdata));// "{\"CarId\":\"\",\"DriverId\":null,\"EmployeeId\":null,\"EmployeeContactNumber\":null,\"EmployeeAddress\":\"Kothrud\",\"EmployeeName\":\"Kunal Kalbande\",\"Status\":\"cancelled\",\"BookingTime\":\"0001-01-01 00:00:00.000 +00:00\",\"Latitude\":18.4994,\"Longitude\":73.8279,\"DriverIds\":[\"adas\",\"asd\"],\"CarType\":\"mini\"}";
            requestForJason.ContentType = "application/json";
            // requestForJason.Accept = "application/json";
            Encoding encoding = new UTF8Encoding();
            byte[] data = encoding.GetBytes(jsonstring);
            using (var stream = await requestForJason.GetRequestStreamAsync())
            {
                // var streamwriter = new StreamWriter(stream);
                //streamwriter.Write(postdata);
                // streamwriter.Flush();
                stream.Write(data, 0, data.Length);
                stream.Dispose();
            }
            string resultResponce = "";

            HttpWebResponse responseLocationMapping = await requestForJason.GetResponseAsync() as HttpWebResponse;
            if (responseLocationMapping != null)
            {
                result = true;
                //string Charset = response.;
                string Charset = "utf-8";
                encoding = Encoding.GetEncoding(Charset);
                var streamReaderResult = new StreamReader(responseLocationMapping.GetResponseStream(), encoding);
                resultResponce = streamReaderResult.ReadToEnd();
                streamReaderResult.Dispose();
            }
            return result;
        }
        public static async Task<bool> AddShipmentStatus(string containerId, ShipmentDetails request)
        {
            bool result = false;
            var jsonstring = JsonConvert.SerializeObject(request);
            string url = String.Format(API + "{0}/shipmentStatus", containerId);
            System.Net.HttpWebRequest requestForJason = (HttpWebRequest)WebRequest.Create(url);
            requestForJason.Method = "POST"; //make an HTTP POST
                                             //  var jsonstring = WebUtility.UrlEncode(JsonConvert.SerializeObject(vehiclebookingdata));// "{\"CarId\":\"\",\"DriverId\":null,\"EmployeeId\":null,\"EmployeeContactNumber\":null,\"EmployeeAddress\":\"Kothrud\",\"EmployeeName\":\"Kunal Kalbande\",\"Status\":\"cancelled\",\"BookingTime\":\"0001-01-01 00:00:00.000 +00:00\",\"Latitude\":18.4994,\"Longitude\":73.8279,\"DriverIds\":[\"adas\",\"asd\"],\"CarType\":\"mini\"}";
            requestForJason.ContentType = "application/json";
            // requestForJason.Accept = "application/json";
            Encoding encoding = new UTF8Encoding();
            byte[] data = encoding.GetBytes(jsonstring);
            using (var stream = await requestForJason.GetRequestStreamAsync())
            {
                // var streamwriter = new StreamWriter(stream);
                //streamwriter.Write(postdata);
                // streamwriter.Flush();
                stream.Write(data, 0, data.Length);
                stream.Dispose();
            }
            string resultResponce = "";

            HttpWebResponse responseLocationMapping = await requestForJason.GetResponseAsync() as HttpWebResponse;
            if (responseLocationMapping != null)
            {
                result = true;
                //string Charset = response.;
                string Charset = "utf-8";
                encoding = Encoding.GetEncoding(Charset);
                var streamReaderResult = new StreamReader(responseLocationMapping.GetResponseStream(), encoding);
                resultResponce = streamReaderResult.ReadToEnd();
                streamReaderResult.Dispose();
            }
            return result;

        }

        public static async Task<bool> UpdateApprovalStatus(string containerId, ApprovalsDetails request)
        {
            bool result = false;
            var jsonstring = JsonConvert.SerializeObject(request);
            string url = String.Format(API + "{0}/approvalStatus", containerId);
            System.Net.HttpWebRequest requestForJason = (HttpWebRequest)WebRequest.Create(url);
            requestForJason.Method = "POST"; //make an HTTP POST
                                             //  var jsonstring = WebUtility.UrlEncode(JsonConvert.SerializeObject(vehiclebookingdata));// "{\"CarId\":\"\",\"DriverId\":null,\"EmployeeId\":null,\"EmployeeContactNumber\":null,\"EmployeeAddress\":\"Kothrud\",\"EmployeeName\":\"Kunal Kalbande\",\"Status\":\"cancelled\",\"BookingTime\":\"0001-01-01 00:00:00.000 +00:00\",\"Latitude\":18.4994,\"Longitude\":73.8279,\"DriverIds\":[\"adas\",\"asd\"],\"CarType\":\"mini\"}";
            requestForJason.ContentType = "application/json";
            // requestForJason.Accept = "application/json";
            Encoding encoding = new UTF8Encoding();
            byte[] data = encoding.GetBytes(jsonstring);
            using (var stream = await requestForJason.GetRequestStreamAsync())
            {
                // var streamwriter = new StreamWriter(stream);
                //streamwriter.Write(postdata);
                // streamwriter.Flush();
                stream.Write(data, 0, data.Length);
                stream.Dispose();
            }
            string resultResponce = "";

            HttpWebResponse responseLocationMapping = await requestForJason.GetResponseAsync() as HttpWebResponse;
            if (responseLocationMapping != null)
            {
                result = true;
                //string Charset = response.;
                string Charset = "utf-8";
                encoding = Encoding.GetEncoding(Charset);
                var streamReaderResult = new StreamReader(responseLocationMapping.GetResponseStream(), encoding);
                resultResponce = streamReaderResult.ReadToEnd();
                streamReaderResult.Dispose();
            }
            return result;

        }
        public static async Task<List<ShipmentDetails>> TrackShipmentsStatus()
        {

            List<ShipmentDetails> shipmentDetails = new List<ShipmentDetails>();
            string url = String.Format(API + "/trackShipments");
            System.Net.HttpWebRequest requestForJason = (HttpWebRequest)WebRequest.Create(url);
            requestForJason.Method = "GET"; //make an HTTP POST
                                             //  var jsonstring = WebUtility.UrlEncode(JsonConvert.SerializeObject(vehiclebookingdata));// "{\"CarId\":\"\",\"DriverId\":null,\"EmployeeId\":null,\"EmployeeContactNumber\":null,\"EmployeeAddress\":\"Kothrud\",\"EmployeeName\":\"Kunal Kalbande\",\"Status\":\"cancelled\",\"BookingTime\":\"0001-01-01 00:00:00.000 +00:00\",\"Latitude\":18.4994,\"Longitude\":73.8279,\"DriverIds\":[\"adas\",\"asd\"],\"CarType\":\"mini\"}";
            requestForJason.ContentType = "application/json";
            // requestForJason.Accept = "application/json";
            Encoding encoding = new UTF8Encoding();
            string resultResponce = "";

            HttpWebResponse responseLocationMapping =  await requestForJason.GetResponseAsync() as HttpWebResponse;
            if (responseLocationMapping != null)
            {
               
                //string Charset = response.;
                string Charset = "utf-8";
                encoding = Encoding.GetEncoding(Charset);
                var streamReaderResult = new StreamReader(responseLocationMapping.GetResponseStream(), encoding);
                resultResponce = streamReaderResult.ReadToEnd();
                streamReaderResult.Dispose();
                shipmentDetails= JsonConvert.DeserializeObject<List<ShipmentDetails>>(resultResponce);
            }
            return shipmentDetails;
        }

        public static async Task<List<TrackingDetails>> TrackShipmentStatus(string containerId)
        {
            List<TrackingDetails> shipmentDetails = new List<TrackingDetails>();
            string url = String.Format(API + "{0}/trackShipment", containerId);
            System.Net.HttpWebRequest requestForJason = (HttpWebRequest)WebRequest.Create(url);
            requestForJason.Method = "GET"; //make an HTTP POST
                                            //  var jsonstring = WebUtility.UrlEncode(JsonConvert.SerializeObject(vehiclebookingdata));// "{\"CarId\":\"\",\"DriverId\":null,\"EmployeeId\":null,\"EmployeeContactNumber\":null,\"EmployeeAddress\":\"Kothrud\",\"EmployeeName\":\"Kunal Kalbande\",\"Status\":\"cancelled\",\"BookingTime\":\"0001-01-01 00:00:00.000 +00:00\",\"Latitude\":18.4994,\"Longitude\":73.8279,\"DriverIds\":[\"adas\",\"asd\"],\"CarType\":\"mini\"}";
            requestForJason.ContentType = "application/json";
            // requestForJason.Accept = "application/json";
            Encoding encoding = new UTF8Encoding();
            string resultResponce = "";

            HttpWebResponse responseLocationMapping = await requestForJason.GetResponseAsync() as HttpWebResponse;
            if (responseLocationMapping != null)
            {

                //string Charset = response.;
                string Charset = "utf-8";
                encoding = Encoding.GetEncoding(Charset);
                var streamReaderResult = new StreamReader(responseLocationMapping.GetResponseStream(), encoding);
                resultResponce = streamReaderResult.ReadToEnd();
                streamReaderResult.Dispose();
                shipmentDetails = JsonConvert.DeserializeObject<List<TrackingDetails>>(resultResponce);
            }
            return shipmentDetails;
        }

        public static async Task<List<ApprovalsDetails>> TrackApproval(string containerId)
        {
            List<ApprovalsDetails> shipmentDetails = new List<ApprovalsDetails>();
            string url = String.Format(API + "{0}/trackApproval", containerId);
            System.Net.HttpWebRequest requestForJason = (HttpWebRequest)WebRequest.Create(url);
            requestForJason.Method = "GET"; //make an HTTP POST
                                            //  var jsonstring = WebUtility.UrlEncode(JsonConvert.SerializeObject(vehiclebookingdata));// "{\"CarId\":\"\",\"DriverId\":null,\"EmployeeId\":null,\"EmployeeContactNumber\":null,\"EmployeeAddress\":\"Kothrud\",\"EmployeeName\":\"Kunal Kalbande\",\"Status\":\"cancelled\",\"BookingTime\":\"0001-01-01 00:00:00.000 +00:00\",\"Latitude\":18.4994,\"Longitude\":73.8279,\"DriverIds\":[\"adas\",\"asd\"],\"CarType\":\"mini\"}";
            requestForJason.ContentType = "application/json";
            // requestForJason.Accept = "application/json";
            Encoding encoding = new UTF8Encoding();
            string resultResponce = "";

            HttpWebResponse responseLocationMapping = await requestForJason.GetResponseAsync() as HttpWebResponse;
            if (responseLocationMapping != null)
            {

                //string Charset = response.;
                string Charset = "utf-8";
                encoding = Encoding.GetEncoding(Charset);
                var streamReaderResult = new StreamReader(responseLocationMapping.GetResponseStream(), encoding);
                resultResponce = streamReaderResult.ReadToEnd();
                streamReaderResult.Dispose();
                shipmentDetails = JsonConvert.DeserializeObject<List<ApprovalsDetails>>(resultResponce);
            }
            return shipmentDetails;
        }

        public static async Task<bool> VerifyWeight(string containerId, ShipmentDetails request)
        {
            bool result = false;
            var jsonstring = JsonConvert.SerializeObject(request);
            string url = String.Format(API + "{0}/verifyWeight", containerId);
            System.Net.HttpWebRequest requestForJason = (HttpWebRequest)WebRequest.Create(url);
            requestForJason.Method = "POST"; //make an HTTP POST
                                             //  var jsonstring = WebUtility.UrlEncode(JsonConvert.SerializeObject(vehiclebookingdata));// "{\"CarId\":\"\",\"DriverId\":null,\"EmployeeId\":null,\"EmployeeContactNumber\":null,\"EmployeeAddress\":\"Kothrud\",\"EmployeeName\":\"Kunal Kalbande\",\"Status\":\"cancelled\",\"BookingTime\":\"0001-01-01 00:00:00.000 +00:00\",\"Latitude\":18.4994,\"Longitude\":73.8279,\"DriverIds\":[\"adas\",\"asd\"],\"CarType\":\"mini\"}";
            requestForJason.ContentType = "application/json";
            // requestForJason.Accept = "application/json";
            Encoding encoding = new UTF8Encoding();
            byte[] data = encoding.GetBytes(jsonstring);
            using (var stream = await requestForJason.GetRequestStreamAsync())
            {
                // var streamwriter = new StreamWriter(stream);
                //streamwriter.Write(postdata);
                // streamwriter.Flush();
                stream.Write(data, 0, data.Length);
                stream.Dispose();
            }
            string resultResponce = "";

            HttpWebResponse responseLocationMapping = await requestForJason.GetResponseAsync() as HttpWebResponse;
            if (responseLocationMapping != null)
            {
                result = true;
                //string Charset = response.;
                string Charset = "utf-8";
                encoding = Encoding.GetEncoding(Charset);
                var streamReaderResult = new StreamReader(responseLocationMapping.GetResponseStream(), encoding);
                resultResponce = streamReaderResult.ReadToEnd();
                streamReaderResult.Dispose();
            }
            return result;

        }

        public static async Task<ShipmentDetailForPayment> GetContainerShipmentDetail(string containerId)
        {
            List<ShipmentDetailForPayment> shipmentDetails = new List<ShipmentDetailForPayment>();
            string url = String.Format(API + "{0}/containerShipmentDetail", containerId);
            System.Net.HttpWebRequest requestForJason = (HttpWebRequest)WebRequest.Create(url);
            requestForJason.Method = "GET"; //make an HTTP POST
                                            //  var jsonstring = WebUtility.UrlEncode(JsonConvert.SerializeObject(vehiclebookingdata));// "{\"CarId\":\"\",\"DriverId\":null,\"EmployeeId\":null,\"EmployeeContactNumber\":null,\"EmployeeAddress\":\"Kothrud\",\"EmployeeName\":\"Kunal Kalbande\",\"Status\":\"cancelled\",\"BookingTime\":\"0001-01-01 00:00:00.000 +00:00\",\"Latitude\":18.4994,\"Longitude\":73.8279,\"DriverIds\":[\"adas\",\"asd\"],\"CarType\":\"mini\"}";
            requestForJason.ContentType = "application/json";
            // requestForJason.Accept = "application/json";
            Encoding encoding = new UTF8Encoding();
            string resultResponce = "";

            HttpWebResponse responseLocationMapping = await requestForJason.GetResponseAsync() as HttpWebResponse;
            if (responseLocationMapping != null)
            {

                //string Charset = response.;
                string Charset = "utf-8";
                encoding = Encoding.GetEncoding(Charset);
                var streamReaderResult = new StreamReader(responseLocationMapping.GetResponseStream(), encoding);
                resultResponce = streamReaderResult.ReadToEnd();
                streamReaderResult.Dispose();
                shipmentDetails = JsonConvert.DeserializeObject<List<ShipmentDetailForPayment>>(resultResponce);
            }
            return shipmentDetails.FirstOrDefault();
        }

        public static async Task<bool> CalculatePayment(string containerId, PaymentDetails request)
        {
            bool result = false;
            var jsonstring = JsonConvert.SerializeObject(request);
            string url = String.Format(API + "{0}/calculatePayment", containerId);
            System.Net.HttpWebRequest requestForJason = (HttpWebRequest)WebRequest.Create(url);
            requestForJason.Method = "POST"; //make an HTTP POST
                                             //  var jsonstring = WebUtility.UrlEncode(JsonConvert.SerializeObject(vehiclebookingdata));// "{\"CarId\":\"\",\"DriverId\":null,\"EmployeeId\":null,\"EmployeeContactNumber\":null,\"EmployeeAddress\":\"Kothrud\",\"EmployeeName\":\"Kunal Kalbande\",\"Status\":\"cancelled\",\"BookingTime\":\"0001-01-01 00:00:00.000 +00:00\",\"Latitude\":18.4994,\"Longitude\":73.8279,\"DriverIds\":[\"adas\",\"asd\"],\"CarType\":\"mini\"}";
            requestForJason.ContentType = "application/json";
            // requestForJason.Accept = "application/json";
            Encoding encoding = new UTF8Encoding();
            byte[] data = encoding.GetBytes(jsonstring);
            using (var stream = await requestForJason.GetRequestStreamAsync())
            {
                // var streamwriter = new StreamWriter(stream);
                //streamwriter.Write(postdata);
                // streamwriter.Flush();
                stream.Write(data, 0, data.Length);
                stream.Dispose();
            }
            string resultResponce = "";

            HttpWebResponse responseLocationMapping = await requestForJason.GetResponseAsync() as HttpWebResponse;
            if (responseLocationMapping != null)
            {
                result = true;
                //string Charset = response.;
                string Charset = "utf-8";
                encoding = Encoding.GetEncoding(Charset);
                var streamReaderResult = new StreamReader(responseLocationMapping.GetResponseStream(), encoding);
                resultResponce = streamReaderResult.ReadToEnd();
                streamReaderResult.Dispose();
            }
            return result;

        }

        public static async Task<List<PaymentDetails>> GetPaymentDetails(string supplierName)
        {
            List<PaymentDetails> paymentDetails = new List<PaymentDetails>();
            string url = String.Format(API + "{0}/getPaymentDetails", supplierName);
            System.Net.HttpWebRequest requestForJason = (HttpWebRequest)WebRequest.Create(url);
            requestForJason.Method = "GET"; //make an HTTP POST
                                            //  var jsonstring = WebUtility.UrlEncode(JsonConvert.SerializeObject(vehiclebookingdata));// "{\"CarId\":\"\",\"DriverId\":null,\"EmployeeId\":null,\"EmployeeContactNumber\":null,\"EmployeeAddress\":\"Kothrud\",\"EmployeeName\":\"Kunal Kalbande\",\"Status\":\"cancelled\",\"BookingTime\":\"0001-01-01 00:00:00.000 +00:00\",\"Latitude\":18.4994,\"Longitude\":73.8279,\"DriverIds\":[\"adas\",\"asd\"],\"CarType\":\"mini\"}";
            requestForJason.ContentType = "application/json";
            // requestForJason.Accept = "application/json";
            Encoding encoding = new UTF8Encoding();
            string resultResponce = "";

            HttpWebResponse responseLocationMapping = await requestForJason.GetResponseAsync() as HttpWebResponse;
            if (responseLocationMapping != null)
            {

                //string Charset = response.;
                string Charset = "utf-8";
                encoding = Encoding.GetEncoding(Charset);
                var streamReaderResult = new StreamReader(responseLocationMapping.GetResponseStream(), encoding);
                resultResponce = streamReaderResult.ReadToEnd();
                streamReaderResult.Dispose();
                paymentDetails = JsonConvert.DeserializeObject<List<PaymentDetails>>(resultResponce);
            }
            return paymentDetails;
        }

        public static async Task<List<ShipmentDetailForPayment>> GetShipmentDetail(string supplierName)
        {
            List<ShipmentDetailForPayment> shipmentDetail = new List<ShipmentDetailForPayment>();
            string url = String.Format(API + "{0}/shipmentDetail", supplierName);
            System.Net.HttpWebRequest requestForJason = (HttpWebRequest)WebRequest.Create(url);
            requestForJason.Method = "GET"; //make an HTTP POST
                                            //  var jsonstring = WebUtility.UrlEncode(JsonConvert.SerializeObject(vehiclebookingdata));// "{\"CarId\":\"\",\"DriverId\":null,\"EmployeeId\":null,\"EmployeeContactNumber\":null,\"EmployeeAddress\":\"Kothrud\",\"EmployeeName\":\"Kunal Kalbande\",\"Status\":\"cancelled\",\"BookingTime\":\"0001-01-01 00:00:00.000 +00:00\",\"Latitude\":18.4994,\"Longitude\":73.8279,\"DriverIds\":[\"adas\",\"asd\"],\"CarType\":\"mini\"}";
            requestForJason.ContentType = "application/json";
            // requestForJason.Accept = "application/json";
            Encoding encoding = new UTF8Encoding();
            string resultResponce = "";

            HttpWebResponse responseLocationMapping = await requestForJason.GetResponseAsync() as HttpWebResponse;
            if (responseLocationMapping != null)
            {

                //string Charset = response.;
                string Charset = "utf-8";
                encoding = Encoding.GetEncoding(Charset);
                var streamReaderResult = new StreamReader(responseLocationMapping.GetResponseStream(), encoding);
                resultResponce = streamReaderResult.ReadToEnd();
                streamReaderResult.Dispose();
                shipmentDetail = JsonConvert.DeserializeObject<List<ShipmentDetailForPayment>>(resultResponce);
            }
            return shipmentDetail;
        }

        public static async Task<int> GetContainerStatus(string containerId)
        {
            int status=9;
            string url = String.Format(API + "{0}/containerStatus", containerId);
            System.Net.HttpWebRequest requestForJason = (HttpWebRequest)WebRequest.Create(url);
            requestForJason.Method = "GET"; //make an HTTP POST
                                            //  var jsonstring = WebUtility.UrlEncode(JsonConvert.SerializeObject(vehiclebookingdata));// "{\"CarId\":\"\",\"DriverId\":null,\"EmployeeId\":null,\"EmployeeContactNumber\":null,\"EmployeeAddress\":\"Kothrud\",\"EmployeeName\":\"Kunal Kalbande\",\"Status\":\"cancelled\",\"BookingTime\":\"0001-01-01 00:00:00.000 +00:00\",\"Latitude\":18.4994,\"Longitude\":73.8279,\"DriverIds\":[\"adas\",\"asd\"],\"CarType\":\"mini\"}";
            requestForJason.ContentType = "application/json";
            // requestForJason.Accept = "application/json";
            Encoding encoding = new UTF8Encoding();
            string resultResponce = "";

            HttpWebResponse responseLocationMapping = await requestForJason.GetResponseAsync() as HttpWebResponse;
            if (responseLocationMapping != null)
            {

                //string Charset = response.;
                string Charset = "utf-8";
                encoding = Encoding.GetEncoding(Charset);
                var streamReaderResult = new StreamReader(responseLocationMapping.GetResponseStream(), encoding);
                resultResponce = streamReaderResult.ReadToEnd();
                streamReaderResult.Dispose();
                status = JsonConvert.DeserializeObject<int>(resultResponce);
            }
            return status;
        }
        public static async Task<int> GetContainerUnit(string containerId)
        {
            int unit = 0;
            string url = String.Format(API + "{0}/containerUnit", containerId);
            System.Net.HttpWebRequest requestForJason = (HttpWebRequest)WebRequest.Create(url);
            requestForJason.Method = "GET"; //make an HTTP POST
                                            //  var jsonstring = WebUtility.UrlEncode(JsonConvert.SerializeObject(vehiclebookingdata));// "{\"CarId\":\"\",\"DriverId\":null,\"EmployeeId\":null,\"EmployeeContactNumber\":null,\"EmployeeAddress\":\"Kothrud\",\"EmployeeName\":\"Kunal Kalbande\",\"Status\":\"cancelled\",\"BookingTime\":\"0001-01-01 00:00:00.000 +00:00\",\"Latitude\":18.4994,\"Longitude\":73.8279,\"DriverIds\":[\"adas\",\"asd\"],\"CarType\":\"mini\"}";
            requestForJason.ContentType = "application/json";
            // requestForJason.Accept = "application/json";
            Encoding encoding = new UTF8Encoding();
            string resultResponce = "";

            HttpWebResponse responseLocationMapping = await requestForJason.GetResponseAsync() as HttpWebResponse;
            if (responseLocationMapping != null)
            {

                //string Charset = response.;
                string Charset = "utf-8";
                encoding = Encoding.GetEncoding(Charset);
                var streamReaderResult = new StreamReader(responseLocationMapping.GetResponseStream(), encoding);
                resultResponce = streamReaderResult.ReadToEnd();
                streamReaderResult.Dispose();
                unit = JsonConvert.DeserializeObject<int>(resultResponce);
            }
            return unit;
        }
        public static async Task<string> GetContainerTimespan(string containerId)
        {
            string time = string.Empty;
            string url = String.Format(API + "{0}/containerTimespan", containerId);
            System.Net.HttpWebRequest requestForJason = (HttpWebRequest)WebRequest.Create(url);
            requestForJason.Method = "GET"; //make an HTTP POST
                                            //  var jsonstring = WebUtility.UrlEncode(JsonConvert.SerializeObject(vehiclebookingdata));// "{\"CarId\":\"\",\"DriverId\":null,\"EmployeeId\":null,\"EmployeeContactNumber\":null,\"EmployeeAddress\":\"Kothrud\",\"EmployeeName\":\"Kunal Kalbande\",\"Status\":\"cancelled\",\"BookingTime\":\"0001-01-01 00:00:00.000 +00:00\",\"Latitude\":18.4994,\"Longitude\":73.8279,\"DriverIds\":[\"adas\",\"asd\"],\"CarType\":\"mini\"}";
            requestForJason.ContentType = "application/json";
            // requestForJason.Accept = "application/json";
            Encoding encoding = new UTF8Encoding();
            string resultResponce = "";

            HttpWebResponse responseLocationMapping = await requestForJason.GetResponseAsync() as HttpWebResponse;
            if (responseLocationMapping != null)
            {

                //string Charset = response.;
                string Charset = "utf-8";
                encoding = Encoding.GetEncoding(Charset);
                var streamReaderResult = new StreamReader(responseLocationMapping.GetResponseStream(), encoding);
                resultResponce = streamReaderResult.ReadToEnd();
                streamReaderResult.Dispose();
                time = JsonConvert.DeserializeObject<string>(resultResponce);
            }
            return time;
        }

        public static async Task<string> GetContainerSupplier(string containerId)
        {
            string supplier = string.Empty;
            string url = String.Format(API + "{0}/containerSupplier", containerId);
            System.Net.HttpWebRequest requestForJason = (HttpWebRequest)WebRequest.Create(url);
            requestForJason.Method = "GET"; //make an HTTP POST
                                            //  var jsonstring = WebUtility.UrlEncode(JsonConvert.SerializeObject(vehiclebookingdata));// "{\"CarId\":\"\",\"DriverId\":null,\"EmployeeId\":null,\"EmployeeContactNumber\":null,\"EmployeeAddress\":\"Kothrud\",\"EmployeeName\":\"Kunal Kalbande\",\"Status\":\"cancelled\",\"BookingTime\":\"0001-01-01 00:00:00.000 +00:00\",\"Latitude\":18.4994,\"Longitude\":73.8279,\"DriverIds\":[\"adas\",\"asd\"],\"CarType\":\"mini\"}";
            requestForJason.ContentType = "application/json";
            // requestForJason.Accept = "application/json";
            Encoding encoding = new UTF8Encoding();
            string resultResponce = "";

            HttpWebResponse responseLocationMapping = await requestForJason.GetResponseAsync() as HttpWebResponse;
            if (responseLocationMapping != null)
            {

                //string Charset = response.;
                string Charset = "utf-8";
                encoding = Encoding.GetEncoding(Charset);
                var streamReaderResult = new StreamReader(responseLocationMapping.GetResponseStream(), encoding);
                resultResponce = streamReaderResult.ReadToEnd();
                streamReaderResult.Dispose();
                supplier = JsonConvert.DeserializeObject<string>(resultResponce);
            }
            return supplier;
        }
    }
}
