import { Component, OnInit } from '@angular/core';
import { Category } from '../models/category.model';
import { DataService } from '../service/data.service';
import { Product } from '../models/product.model';
import { Field } from '../models/field.model';
import { FieldValue } from '../models/field-value.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-product-add',
  templateUrl: './product-add.component.html',
  styleUrls: ['./product-add.component.scss']
})
export class ProductAddComponent implements OnInit {

  _dataService: DataService;
  categories:Category[];
  _selectedCategory:Category;
  product:Product = new Product;
  fields:Field [] = [];

  get selectedCategory(): Category {
    return this._selectedCategory;
  }
  set selectedCategory(value: Category) {
      if (value !== this._selectedCategory) {
          this._selectedCategory = value;
          this.onCategoryChange(value)
      }
  }

  constructor(dataService:DataService) {
    this._dataService = dataService;
    this._dataService.getAllCategories().subscribe(data =>{
      console.log(data)
      this.categories =data
    })
    this.product.field_values = [];
  }

  ngOnInit(): void {
  }

  onCategoryChange(newValue){
    this._dataService.getFieldsInCategory(this._selectedCategory.id).subscribe(data =>{
      console.log(data)
      this.fields = data;
      data.length > 0 ?
      data.forEach(field =>{
        this.product.field_values.push(new FieldValue())
      }) : this.product.field_values = [];
    },
    error => {
      console.log(error)
    })
  }

  submitProduct(){
    this._dataService.postProduct(this.product).subscribe(r => {
      console.log(r);
    });
  }

}
