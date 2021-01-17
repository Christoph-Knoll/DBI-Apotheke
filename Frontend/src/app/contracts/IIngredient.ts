export interface IIngredient{
  name: string;
  amount: number;
  unit: Unit;

}

export enum Unit {
  Stk,
  g,
  mg,
  ml,
}
