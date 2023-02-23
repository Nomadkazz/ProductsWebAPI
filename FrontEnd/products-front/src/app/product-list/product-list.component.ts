import { Category } from '../models/category.model';
import { Component, OnInit } from '@angular/core';
import { DataService } from '../service/data.service';
import { Product } from '../models/product.model';
import { Field } from '../models/field.model';
import { FieldValue } from '../models/field-value.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-product',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.scss']
})
export class ProductListComponent implements OnInit {

  _dataService: DataService;
  products:Product[];
  categories:Category[];
  _selectedCategory:Category=null;
  selectedField:Field=null;

  constructor(dataService:DataService, router:Router) {
    this._dataService = dataService;

    this.getAllProducts();

    this._dataService.getAllCategories().subscribe(data =>{
      console.log(data)
      this.categories =data
    })

  }

  ngOnInit(): void {
  }

  getAllProducts(){
    this._dataService.getAllProducts().subscribe(data =>{
      console.log(data)
      this.products =data
    })
  }

  onCategoryChange(newValue){
    console.log(newValue)
    if(this._selectedCategory == null){
      this.getAllProducts()
    }else{
      this.getProductsByCategory()
    }
  }

  getProductsByCategory(){
    this._dataService.getProductsByCategory(this._selectedCategory.id).subscribe(data =>{
      this.products = data
    },
    error => {
      console.log(error)
      this.products = null
    })
  }
}
