import { Component, OnInit } from '@angular/core';
import {ProductService} from '../services/product.service';
import {ProductInfoService} from '../services/product-info.service';
import {IProduct} from '../contracts/IProduct';
import {IProductInfo} from '../contracts/IProductInfo';
import {StorageService} from '../services/storage.service';
import {IStorage} from '../contracts/IStorage';

@Component({
  selector: 'app-prdoucts',
  templateUrl: './prdoucts.component.html',
  styleUrls: ['./prdoucts.component.css']
})
export class PrdouctsComponent implements OnInit {

  products: IProduct[];
  productInfos: IProductInfo[];
  storage: IStorage[];
  constructor(private productService: ProductService,
              private productInfoService: ProductInfoService,
              private storageService: StorageService) { }

  async ngOnInit(): Promise<void> {
    this.products = await this.productService.getAll().toPromise();
    this.productInfos = await this.productInfoService.getAll().toPromise();
    this.storage = await this.storageService.getAll().toPromise();
  }

  getProductInfoById(productInfoId: number): IProductInfo {
    this.productInfos.filter(i => {
      if (i.id === productInfoId){return i; }
    });
    return null;
  }
}
