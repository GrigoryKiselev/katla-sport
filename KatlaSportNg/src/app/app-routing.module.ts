import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MainPageComponent } from 'app/main-page/main-page.component';
import { HiveFormComponent } from './hive-management/forms/hive-form.component';
import { HiveSectionFormComponent } from './hive-management/forms/hive-section-form.component';
import { HiveListComponent } from './hive-management/lists/hive-list.component';
import { HiveSectionListComponent } from './hive-management/lists/hive-section-list.component';
import { ProductCategoryFormComponent } from './product-management/forms/product-category-form.component';
import { ProductFormComponent } from './product-management/forms/product-form.component';
import { ProductCategoryListComponent } from './product-management/lists/product-category-list.component';
import { ProductCategoryProductListComponent } from './product-management/lists/product-category-product-list.component';
import { ProductListComponent } from './product-management/lists/product-list.component';
import { OrderFormComponent } from './order-management/forms/order-form.component';
import { TransactionFormComponent } from './order-management/forms/transaction-form.component';
import { OrderListComponent } from './order-management/lists/order-list.component';
import { TransactionListComponent } from './order-management/lists/transaction-list.component';

const routes: Routes = [
  { path: '', redirectTo: '/main', pathMatch: 'full' },
  { path: 'main', component: MainPageComponent },
  { path: 'categories', component: ProductCategoryListComponent },
  { path: 'category', component: ProductCategoryFormComponent },
  { path: 'category/:id', component: ProductCategoryFormComponent },
  { path: 'category/:id/products', component: ProductCategoryProductListComponent },
  { path: 'products', component: ProductListComponent },
  { path: 'product/:id', component: ProductFormComponent },
  { path: 'category/:categoryId/product/:id', component: ProductFormComponent },
  { path: 'hives', component: HiveListComponent },
  { path: 'hive', component: HiveFormComponent },
  { path: 'hive/:id', component: HiveFormComponent },
  { path: 'hive/:id/sections', component: HiveSectionListComponent },
  { path: 'section/:id', component: HiveSectionFormComponent },
  { path: 'hive/:hiveId/section/:id', component: HiveSectionFormComponent},
  { path: 'hive/:hiveId/section', component: HiveSectionFormComponent},
  { path: 'orders', component: OrderListComponent },
  { path: 'order', component: OrderFormComponent },
  { path: 'order/:id', component: OrderFormComponent },
  { path: 'order/:id/transactions', component: TransactionListComponent },
  { path: 'transaction/:id', component: TransactionFormComponent },
  { path: 'order/:orderId/transaction/:id', component: TransactionFormComponent},
  { path: 'order/:orderId/transaction', component: TransactionFormComponent}
]

@NgModule({
  imports: [ RouterModule.forRoot(routes) ],
  exports: [ RouterModule ]
})
export class AppRoutingModule { }
