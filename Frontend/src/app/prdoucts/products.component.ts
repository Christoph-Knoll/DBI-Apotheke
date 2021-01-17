import {Component, OnInit} from '@angular/core';
import {ProductService} from '../services/product.service';
import {ProductInfoService} from '../services/product-info.service';
import {IProduct} from '../contracts/IProduct';
import {IProductInfo, State} from '../contracts/IProductInfo';
import {StorageService} from '../services/storage.service';
import {IStorage} from '../contracts/IStorage';
import {IIngredient, Unit} from '../contracts/IIngredient';


@Component({
  selector: 'app-prdoucts',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {


  products: IProduct[] = [
    {
      pzn: 1,
      price: 1.20,
      amount: 300,
      unit: Unit.Stk,
      productInfoId: 1,
    },
    {
      pzn: 2,
      price: 1.20,
      amount: 150,
      unit: Unit.mg,
      productInfoId: 1,
    },
    {
      pzn: 3,
      price: 25,
      amount: 222,
      unit: Unit.mg,
      productInfoId: 2,
    },
  ];
  productInfos: IProductInfo[] = [
    {
      id: 1,
      name: 'Aspirin Complex',
      brand: 'Aspirin',
      state: State.Solid,
      ingredients: [
        {
          name: 'Vitamin A',
          amount: 100,
          unit: Unit.ml,
        },
        {
          name: 'Vitamin B',
          amount: 20,
          unit: Unit.ml,
        },
      ]
    },
    {
      id: 2,
      name: 'Aspirin Easy',
      brand: 'Aspirin',
      state: State.Solid,
      ingredients: [
        {
          name: 'Vitamin A',
          amount: 100,
          unit: Unit.ml,
        },
        {
          name: 'Vitamin B',
          amount: 20,
          unit: Unit.ml,
        },
      ]
    }
  ];
  storage: IStorage[] = [
    {
      pzn: 1,
      amount: 10,
      storageSite: 'Linz',
    },
    {
      pzn: 1,
      amount: 3,
      storageSite: 'Wels',
    },
    {
      pzn: 2,
      amount: 2,
      storageSite: 'Leonding',
    },
    {
      pzn: 3,
      amount: 12,
      storageSite: 'Linz',
    }
  ];
  constructor(private productService: ProductService,
              private productInfoService: ProductInfoService,
              private storageService: StorageService) { }

  async ngOnInit(): Promise<void> {
    // this.products = await this.productService.getAll().toPromise();
    // this.productInfos = await this.productInfoService.getAll().toPromise();
    // this.storage = await this.stor ageService.getAll().toPromise();
  }

  getProductInfoById(productInfoId: number): IProductInfo {
    return this.productInfos.find(i => i.id === productInfoId); }

  getStorageInfoByPZN(pzn: number): number{
    let amountStoredProducts = 0;
    this.storage.filter(i => {
      if (i.pzn === pzn){
        amountStoredProducts += i.amount;
      }
    });
    return amountStoredProducts;
  }

  public get unit(): typeof Unit {
    return Unit;
  }

  onProductInfoClick(productInfoId: number): void {
    console.log(productInfoId);
  }
}
