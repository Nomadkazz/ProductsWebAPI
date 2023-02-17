import { element } from 'protractor';
import { Field } from './../models/field.model';
import { Category } from './../models/category.model';
import { Component, OnInit } from '@angular/core';
import { DataService } from '../service/data.service';

@Component({
  selector: 'app-category-add',
  templateUrl: './category-add.component.html',
  styleUrls: ['./category-add.component.scss']
})
export class CategoryAddComponent implements OnInit {
  category:Category = new Category;
  _dataService: DataService;
  fields:Field [] = [];

  constructor(dataService:DataService) {
    this._dataService = dataService
  }

  ngOnInit(): void {
  }

  newField():void{
    var field = new Field;
    this.fields.push(field);
  }

  removeField():void{
    this.fields.pop();
  }

  submitCategory():void{
    console.log(this.category);
    console.log(this.fields);
    this._dataService.postCategory(this.category).subscribe(r => {
      console.log(r);
      if(this.fields.length > 0 && r != 0){
        this.fields.forEach(field => {
          this._dataService.postField(field, r)
        })
      };
    });

  }

}
