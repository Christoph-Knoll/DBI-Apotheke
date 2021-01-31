import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {RouterModule, Routes} from '@angular/router';
import {AddRecipeComponent} from './add-recipe/add-recipe.component';
import {RedeemRecipeComponent} from './redeem-recipe/redeem-recipe.component';
import { ProductsComponent } from './prdoucts/products.component';

const routes: Routes = [
  { path: '', redirectTo: '/products', pathMatch: 'full'},
  { path: 'addRecipe', component: AddRecipeComponent },
  { path: 'redeemRecipe', component: RedeemRecipeComponent},
  { path: 'products',  component: ProductsComponent},

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
