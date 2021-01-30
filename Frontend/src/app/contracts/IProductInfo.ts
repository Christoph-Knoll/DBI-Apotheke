import {IIngredient} from './IIngredient';

export interface IProductInfo{
  id: number;
  name: string;
  brand: string;
  state: State;
  ingredients: IIngredient[];

}

export enum State{
  Solid,
  Liquid
}
