import { Product } from './../models/product.model';
import { FieldValue } from './../models/field-value.model';
import { Field } from './../models/field.model';
import { Category } from '../models/category.model';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { catchError, retry } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class DataService {
  backend_url = "https://localhost:7164/api/"
  constructor(private http: HttpClient) { }

  getAllCategories():Observable<Category[]>{
    return this.http.get<Category[]>(this.backend_url + "categories");
  }

  getAllProducts():Observable<Product[]>{
    return this.http.get<Product[]>(this.backend_url + "products");
  }

  getFields():Observable<Field[]>{
    return this.http.get<Field[]>(this.backend_url + "fields");
  }

  getProductsByCategory(id:number):Observable<Product[]>{
    return this.http.get<Product[]>(this.backend_url + "products/category/" + id);
  }

  getProductsByFields(id:number):Observable<Product[]>{
    return this.http.get<Product[]>(this.backend_url + "products/field/" + id);
  }

  getFieldsInCategory(id:number):Observable<Product[]>{
    return this.http.get<Product[]>(this.backend_url + "fields/category/" + id);
  }

  postCategory(category:Category):Observable<number>{
    return this.http.post<number>(this.backend_url + "categories/", category)
    .pipe(
      //catchError(this.handleError('addCategory', category))
    );
  }

  postField(field:Field,categoryId:number):Observable<number>{
    return this.http.post<number>(this.backend_url + "fields/category/" + categoryId, field)
    .pipe(
      //catchError(this.handleError('addCategory', category))
    );
  }

  postProduct(product:Product):Observable<number>{
    return this.http.post<number>(this.backend_url + "products/", product)
    // .pipe(
    //   //catchError(this.handleError('addCategory', category))
    // );
  }
}
