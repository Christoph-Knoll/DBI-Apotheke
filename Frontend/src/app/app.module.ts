import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import {RouterModule} from '@angular/router';
import { AddRecipeComponent } from './add-recipe/add-recipe.component';
import { AppRoutingModule } from './app-routing.module';
import { RedeemRecipeComponent } from './redeem-recipe/redeem-recipe.component';
import { PrdouctsComponent } from './prdoucts/prdoucts.component';
import { ProductDetailsComponent } from './product-details/product-details.component';
import {FormsModule} from '@angular/forms';
import {HttpClient, HttpClientModule} from '@angular/common/http';

@NgModule({
  declarations: [
    AppComponent,
    AddRecipeComponent,
    RedeemRecipeComponent,
    PrdouctsComponent,
    ProductDetailsComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    RouterModule,
    AppRoutingModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
