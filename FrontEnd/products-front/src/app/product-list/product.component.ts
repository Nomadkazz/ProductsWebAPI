import { Category } from '../models/category.model';
import { Component, OnInit } from '@angular/core';
import { DataService } from '../service/data.service';
import { Product } from '../models/product.model';
import { Field } from '../models/field.model';
import { FieldValue } from '../models/field-value.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.scss']
})
export class ProductComponent implements OnInit {

  _dataService: DataService;
  products:Product[];
  categories:Category[];
  fields:Field[];
  _selectedCategory:Category;
  selectedField:Field;

  get selectedCategory(): Category {
    return this._selectedCategory;
  }
  set selectedCategory(value: Category) {
      if (value !== this._selectedCategory) {
          this._selectedCategory = value;
          this.getFields()
          this.getProductsByCategory()
      }
  }

  constructor(dataService:DataService) {
    this._dataService = dataService;
    this._dataService.getAllProducts().subscribe(data =>{
      console.log(data)
      this.products =data
    })
    this._dataService.getAllCategories().subscribe(data =>{
      console.log(data)
      this.categories =data
      //this.categories.length > 0 ? this.selectedCategory = this.categories[0] : this.selectedCategory = null ;
    })

  }

  ngOnInit(): void {
  }

  getFields(){
    this._dataService.getFieldsInCategory(this._selectedCategory.id).subscribe(data =>{
      console.log(data)
      this.fields =data
      this.fields.length > 0 ? this.selectedField = this.fields[0] : this.selectedField = null ;
    },
    error => {
      console.log(error)
      this.selectedField = null
    })
  }

  onCategoryChange(newValue){
    this.getFields()
    this.getProductsByCategory()
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
