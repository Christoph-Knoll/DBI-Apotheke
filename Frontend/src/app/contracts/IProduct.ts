import {Unit} from './IIngredient';

export interface IProduct{
  pzn: number;
  price: number;
  amount: number;
  unit: Unit;
  productInfoId: string;
}

