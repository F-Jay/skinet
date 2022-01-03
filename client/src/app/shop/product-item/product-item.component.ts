import { Component, Input, OnInit } from '@angular/core';
import { BasketService } from 'src/app/basket/basket.service';
import { IProduct } from 'src/app/shared/models/product';

@Component({
  selector: 'app-product-item',
  templateUrl: './product-item.component.html',
  styleUrls: ['./product-item.component.scss']
})
export class ProductItemComponent implements OnInit {
  @Input() product: IProduct; // Input Properties are used to pass data between conponents (Parent to Child)

  constructor(private basketService: BasketService) { }

  ngOnInit(): void {
  }

  addItemToBasket(){
    this.basketService.addItemToBasket(this.product);
  }

}
