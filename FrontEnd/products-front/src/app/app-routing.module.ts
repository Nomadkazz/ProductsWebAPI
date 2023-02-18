import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { CategoryComponent } from './category-list/category-list.component';
import { ProductListComponent } from './product-list/product-list.component';
import { CategoryAddComponent } from './category-add/category-add.component';
import { ProductAddComponent } from './product-add/product-add.component';
import { ProductItemComponent } from './product-item/product-item.component';

const routes: Routes = [
  { path: '', redirectTo: '/categories', pathMatch: 'full' },
  { path: 'categories', component: CategoryComponent },
  { path: 'categories/add', component: CategoryAddComponent },
  { path: 'products',  component: ProductListComponent },
  { path: 'products/add',  component: ProductAddComponent },
  { path: 'products/:id', component: ProductItemComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
