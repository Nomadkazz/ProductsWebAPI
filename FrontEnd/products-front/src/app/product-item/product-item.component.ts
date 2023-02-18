import { Component, OnInit } from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import { DataService } from '../service/data.service';
import { Category } from '../models/category.model';
import { Product } from '../models/product.model';
import { Field } from '../models/field.model';
import { FieldValue } from '../models/field-value.model';

@Component({
  selector: 'app-product-item',
  templateUrl: './product-item.component.html',
  styleUrls: ['./product-item.component.scss']
})
export class ProductItemComponent implements OnInit {
  productId:number;
  product:Product = new Product;
  fields:Field [] = [];

  constructor(private route: ActivatedRoute, private _dataService:DataService,) {
    this.route.params.subscribe( params => {
      console.log(params)
      this.productId = params['id'];

      this._dataService.getProductsById(this.productId).subscribe(product =>{
        console.log(product)

        this._dataService.getFieldsInCategory(product.categoryId).subscribe(fields =>{
          console.log(fields)
          this.fields = fields;
          this.product = product;
        },
        error => {
          console.log(error)
        })
      })
    });

   }

  ngOnInit(): void {

  }

}
