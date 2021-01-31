import { BrowserModule } from '@angular/platform-browser';
import {LOCALE_ID, NgModule} from '@angular/core';


import { AppComponent } from './app.component';
import {RouterModule} from '@angular/router';
import { AddRecipeComponent } from './add-recipe/add-recipe.component';
import { AppRoutingModule } from './app-routing.module';
import { RedeemRecipeComponent } from './redeem-recipe/redeem-recipe.component';
import { ProductsComponent } from './prdoucts/products.component';
import {FormsModule} from '@angular/forms';
import {HttpClient, HttpClientModule} from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

@NgModule({
  declarations: [
    AppComponent,
    AddRecipeComponent,
    RedeemRecipeComponent,
    ProductsComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    RouterModule,
    AppRoutingModule,
    FormsModule,
    BrowserAnimationsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
