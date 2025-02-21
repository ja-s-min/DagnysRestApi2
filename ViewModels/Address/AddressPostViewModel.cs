using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dagnys_api.ViewModels;
using System.Text.Json.Serialization;

namespace dagnys_api.ViewModels.Address;
public enum AddressTypeEnum
{
  Delivery = 1,
  Invoice = 2,
  Distribution = 3
}
public class AddressPostViewModel
{
  public string AddressLine { get; set; }
  public string PostalCode { get; set; }
  public string City { get; set; }
  [JsonConverter(typeof(JsonStringEnumConverter))]
  public AddressTypeEnum AddressType { get; set; }

}