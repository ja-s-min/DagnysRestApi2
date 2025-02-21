﻿using dagnys_api.ViewModels.Address;

namespace dagnys_api;

public class CustomerViewModel : CustomerBaseViewModel
{
  public int Id { get; set; }
  public IList<AddressViewModel> Addresses { get; set; }
}
