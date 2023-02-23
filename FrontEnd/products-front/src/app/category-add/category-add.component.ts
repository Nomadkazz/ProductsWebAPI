import { element } from 'protractor';
import { Field } from './../models/field.model';
import { Category } from './../models/category.model';
import { Component, OnInit } from '@angular/core';
import { DataService } from '../service/data.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-category-add',
  templateUrl: './category-add.component.html',
  styleUrls: ['./category-add.component.scss']
})
export class CategoryAddComponent implements OnInit {
  router:Router;

  category:Category = new Category;
  _dataService: DataService;
  fields:Field [] = [];

  constructor(dataService:DataService,router:Router) {
    this.router = router
    this._dataService = dataService
    this.category.fields = []
  }

  ngOnInit(): void {
  }

  newField():void{
    var field = new Field;
    this.category.fields.push(field);
  }

  removeField():void{
    this.category.fields.pop();
  }

  submitCategory():void{
    console.log(this.category);
    console.log(this.category.fields);
    this._dataService.postCategory(this.category).subscribe(r => {
      console.log(r);
      if(this.category.fields.length > 0 && r != 0){
        this.category.fields.forEach(field => {
          this._dataService.postField(field, r).subscribe(data =>{
            console.log(data);
            this.router.navigate(['/categories']);
          })
        })
      };
    }, err => {
      alert("Fill in the fields");
    });
  }


}
