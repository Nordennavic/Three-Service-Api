using erthsobesapi.Model;
using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace erthsobesapi.Controllers
{
    public partial class OrderController
    {
        public async Task<Guid> CreateObject(string dataType)
        {
            var response = await _httpClient.GetAsync("http://172.23.0.2:6500/api/GetObjectInfo?type=" + dataType);
            dynamic newObject = response.Content.ReadAsAsync<ExpandoObject>().Result;
            var newOrder = new Order
            {
                product_id = Guid.Parse(newObject.data.id),
                type = newObject.dataType,
                cost = Convert.ToDecimal(newObject.data.cost)
            };
            switch (newObject.dataType)
            {
                case "phone":
                    newOrder.phoneNumber = newObject.data.phone;
                    break;
                case "email":
                    newOrder.email = newObject.data.email;
                    break;
                default:
                    newOrder.value = newObject.data.value;
                    break;
            }
            try
            {
                newOrder.attachment_id = newObject.data.file.id;
                await _dataAccessProvider.AddAttachment(new Attachment { id = newObject.data.file.id, hash = newObject.data.file.hash });
                await _dataAccessProvider.AddOrder(newOrder);
            }
            catch (RuntimeBinderException)
            {
                await _dataAccessProvider.AddOrder(newOrder);
            }
            return newOrder.product_id;
        }
    }
}
