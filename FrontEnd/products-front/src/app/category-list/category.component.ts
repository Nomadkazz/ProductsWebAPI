import { Component, OnInit } from '@angular/core';
import { DataService } from '../service/data.service';
import { Category } from '../models/category.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.scss']
})
export class CategoryComponent implements OnInit {
  _dataService: DataService;
  categories:Category[];

  constructor( dataService:DataService) {
    this._dataService = dataService;
    this._dataService.getAllCategories().subscribe(data =>{
      console.log(data)
      this.categories =data
    })
   }

  ngOnInit(): void {

  }


}
