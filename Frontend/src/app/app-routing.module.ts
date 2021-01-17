import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {RouterModule, Routes} from '@angular/router';
import {AddRecipeComponent} from './add-recipe/add-recipe.component';
import {RedeemRecipeComponent} from './redeem-recipe/redeem-recipe.component';
import { PrdouctsComponent } from './prdoucts/prdoucts.component';
import {ProductDetailsComponent} from './product-details/product-details.component';

const routes: Routes = [
  { path: 'addRecipe', component: AddRecipeComponent },
  { path: 'redeemRecipe', component: RedeemRecipeComponent},
  { path: 'products',  children: [
      { path: '', component: PrdouctsComponent},
      { path: ':id', component: ProductDetailsComponent}
    ]},

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
