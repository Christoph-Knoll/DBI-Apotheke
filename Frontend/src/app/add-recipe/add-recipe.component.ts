import { Component, OnInit } from '@angular/core';
import {RecipeService} from '../services/recipe.service';
import {IRecipe} from '../contracts/IRecipe';

@Component({
  selector: 'app-add-recipe',
  templateUrl: './add-recipe.component.html',
  styleUrls: ['./add-recipe.component.css']
})
export class AddRecipeComponent implements OnInit {

  values: number[] = [];
  firstName: string;
  lastName: string;
  address: string;
  zipCode: string;
  city: string;
  issuer: string;
  recipe: IRecipe = {} as IRecipe;
  constructor(private recipeService: RecipeService) { }

  ngOnInit(): void {
    this.values.push(null);
  }

  removeValue(i): void{
    this.values.splice(i, 1);
  }

  addValue(): void{
    this.values.push(null);
  }


  createRecipe(): void {
    this.recipe.name = this.firstName + ' ' + this.lastName;
    this.recipe.address = this.address + ',' + this.zipCode + ' ' + this.city;
    this.recipe.issuer = this.issuer;
    this.recipe.pzns = this.values;
    console.log(this.recipe);
    this.recipeService.save(this.recipe);
  }

  customTrackBy(index: number, obj: any): any {
    return index;
  }
}
