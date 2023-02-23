import { Component, OnInit } from '@angular/core';
import { Category } from '../models/category.model';
import { DataService } from '../service/data.service';
import { Product } from '../models/product.model';
import { Field } from '../models/field.model';
import { FieldValue } from '../models/field-value.model';
import { Router } from '@angular/router';
import { NgForm} from '@angular/forms';

@Component({
  selector: 'app-product-add',
  templateUrl: './product-add.component.html',
  styleUrls: ['./product-add.component.scss']
})
export class ProductAddComponent implements OnInit {

  productForm: NgForm;
  router:Router;
  _dataService: DataService;

  categories:Category[];
  _selectedCategory:Category;
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
    this.product.fieldValues = [];
    this._selectedCategory.fields.forEach(field =>{
      this.product.fieldValues.push(new FieldValue())
    })
  }

  submitProduct(){
    this.product.id = 0
    this.product.categoryId = this._selectedCategory.id

    console.log(this.product)
    this._dataService.postProduct(this.product).subscribe(r => {
      console.log(r);
      this.router.navigate(['/products']);
    }, error =>{
      alert("Fill in the fields")
    });
  }

  processFile(image){
    console.log(image)
    console.log(image.target.files[0]);

    let reader = new FileReader();
    reader.readAsDataURL(image.target.files[0]);
    reader.onload = (ev) =>{
      console.log(reader.result)
      this.product.photoUrl = reader.result.toString();
    }
  }
}
