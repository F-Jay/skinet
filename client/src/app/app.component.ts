import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IProduct } from './models/product';
import { IPagination } from './models/pagination';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{
  // Properties
  title = 'Skinet';
  products: IProduct[];

  constructor(private http: HttpClient){ }

  // Life Cycle Methods
  ngOnInit(): void 
  {
    // Call API - With end point
    this.http.get('https://localhost:5001/api/products?pageSize=50').subscribe((response: IPagination) => 
    {
      this.products = response.data; // Set response from API to the Products Property to be accesible in the Html Conponent.
    }, error =>
    {
      console.log(error);
    }); 
  }

  // Life Cycle Hooks
}
