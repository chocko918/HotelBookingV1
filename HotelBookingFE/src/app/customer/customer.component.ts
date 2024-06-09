import { Component, OnInit } from '@angular/core';
import { ServicesService } from '../services.service'; 


@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html',
  styleUrls: ['./customer.component.css']
})
export class CustomerComponent implements OnInit{

  constructor(private service: ServicesService) { }

  CustomerList: any = [];

  ngOnInit(): void {
    this.refreshCustomerList()
    
  }

  refreshCustomerList() {

    console.log("REFRESHED")
    this.service.getCustomerList().subscribe(data => {
      this.CustomerList = data;
      console.log(this.CustomerList)
    });
  }

}
