import { Component, OnInit,ChangeDetectorRef } from '@angular/core';
import { DataService } from '../service/data.service';
import { Category } from '../models/category.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-category',
  templateUrl: './category-list.component.html',
  styleUrls: ['./category-list.component.scss']
})
export class CategoryComponent implements OnInit {
  _dataService: DataService;
  categories:Category[];
  _changeDetectorRef:ChangeDetectorRef

  constructor( dataService:DataService) {
    this._dataService = dataService;

    this.getCategories()
   }

  ngOnInit(): void {

  }

  getCategories(){
    this._dataService.getAllCategories().subscribe(data =>{
      console.log(data)
      this.categories =data
    })
  }

  deleteCategory(category){
    console.log(category)
    this._dataService.deleteCategory(category.id).subscribe(data =>{
      this.getCategories()
    })
  }

}
