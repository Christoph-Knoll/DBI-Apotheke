import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {RouterModule, Routes} from '@angular/router';
import {AddRecipeComponent} from './add-recipe/add-recipe.component';
import {RedeemRecipeComponent} from './redeem-recipe/redeem-recipe.component';
import { ProductsComponent } from './prdoucts/products.component';
import {ProductDetailsComponent} from './product-details/product-details.component';

const routes: Routes = [
  { path: 'addRecipe', component: AddRecipeComponent },
  { path: 'redeemRecipe', component: RedeemRecipeComponent},
  { path: 'products',  component: ProductsComponent},
  { path: 'productInfo/:id', component: ProductDetailsComponent},

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
