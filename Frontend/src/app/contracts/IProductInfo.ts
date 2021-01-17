import {IIngredient} from './IIngredient';

export interface IProductInfo{
  name: string;
  brand: string;
  state: State;
  ingredients: IIngredient[];

}

enum State{
  Solid,
  Liquid
}
