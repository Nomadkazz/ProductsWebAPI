import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { CategoryComponent } from './category-list/category.component';
import { ProductComponent } from './product-list/product.component';
import { CategoryAddComponent } from './category-add/category-add.component';
import { ProductAddComponent } from './product-add/product-add.component';

const routes: Routes = [
  { path: '', redirectTo: '/categories', pathMatch: 'full' },
  { path: 'categories', component: CategoryComponent },
  { path: 'categories/add', component: CategoryAddComponent },
  { path: 'products',  component: ProductComponent },
  { path: 'products/add',  component: ProductAddComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
