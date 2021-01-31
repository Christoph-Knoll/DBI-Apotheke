import { Component, OnInit } from '@angular/core';
import {RecipeService} from '../services/recipe.service';
import {IRecipe} from '../contracts/IRecipe';
import {IIngredient, Unit} from '../contracts/IIngredient';
import {IProductInfo, State} from '../contracts/IProductInfo';
import {IProduct} from '../contracts/IProduct';
import {ProductService} from '../services/product.service';
import {ProductInfoService} from '../services/product-info.service';

@Component({
  selector: 'app-redeem-recipe',
  templateUrl: './redeem-recipe.component.html',
  styleUrls: ['./redeem-recipe.component.css']
})


export class RedeemRecipeComponent implements OnInit {
  recipeId: string;
  foundRecipe: boolean;
  recipe: IRecipe;
  address: string[];
  products: IProduct[];
  productsFiltered: IProduct[];
  currentProductInfo: IProductInfo;

  constructor(private recipeServices: RecipeService,
              private productService: ProductService,
              private productInfoService: ProductInfoService) { }

  ngOnInit(): void {
    this.foundRecipe = undefined;
  }

  async onSearch(): Promise<void> {
    this.recipe = (await this.recipeServices.getAll().toPromise())[1];
    this.foundRecipe = this.recipe !== undefined;
    if (this.foundRecipe) {
      this.address = this.recipe.address.split(';');
      this.products = await this.productService.getAll().toPromise();
      this.products.filter(product => {
        this.recipe.pzns.filter(pzn => {
          if (product.pzn === pzn){
            this.productsFiltered.push(product);
          }
        });
      });
    }
  }


  // @ts-ignore
  async getProductInfoById(productInfoId: number): IProductInfo {
    return this.currentProductInfo = await this.productInfoService.get(productInfoId).toPromise() as IProductInfo;
  }

  public get unit(): typeof Unit {
    return Unit;
  }
}
