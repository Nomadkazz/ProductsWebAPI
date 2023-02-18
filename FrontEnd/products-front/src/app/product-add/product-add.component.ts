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

  router:Router;

  _dataService: DataService;
  categories:Category[];
  _selectedCategory:Category;
  fields:Field [] = [];
  product:Product = new Product;

  constructor(dataService:DataService, router:Router) {
    this.router = router
    this._dataService = dataService;
    this._dataService.getAllCategories().subscribe(data =>{
      console.log(data)
      this.categories =data
    })
    this.product.fieldValues = [];
  }

  ngOnInit(): void {
  }

  onCategoryChange(newValue){
    this._dataService.getFieldsInCategory(this._selectedCategory.id).subscribe(data =>{
      console.log(data)
      this.fields = data;
      data.length > 0 ?
      data.forEach(field =>{
        this.product.fieldValues.push(new FieldValue())
      }) : this.product.fieldValues = [];
    },
    error => {
      console.log(error)
    })
  }

  submitProduct(){
    this.product.id = 0
    this.product.categoryId = this._selectedCategory.id
    //this.fields == null ? this.product.fieldValues = [] : this.product.fieldValues;

    console.log(this.product)
    this.product.name == undefined ||
      this.product.description == undefined  ||
        this.product.price == undefined  ||
          this.product.photoUrl == undefined ?  alert("Fill in the fields") :
    this._dataService.postProduct(this.product).subscribe(r => {
      console.log(r);
      this.router.navigate(['/products']);
    });
  }

}
