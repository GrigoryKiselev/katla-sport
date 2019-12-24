import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Transaction } from '../models/transaction';
import { TransactionService } from '../services/transaction.service';

@Component({
  selector: 'app-transaction-form'/*,
  templateUrl: './transaction-form.component.html' */
})
export class TransactionFormComponent implements OnInit {
  public transaction = new Transaction(0, "", "", "", 0, false);
  public existed = false;

   constructor(
    private route: ActivatedRoute,
    private router: Router,
    private transactionService: TransactionService
  ) { }

   ngOnInit() : void {
    this.route.params.subscribe(p => {
      if (p['id'] === undefined) {
        this.transaction.storeOrderId = p['orderId'];
        return;
      }
       this.transactionService.getTransaction(p['id']).subscribe(h => {
         this.transaction = h;
        this.transaction.storeOrderId = p['orderId'];
       });
      this.existed = true;
    });  
  }

   navigateToTransactions() : void {
    this.router.navigate([`/order/${this.transaction.storeOrderId}/transactions`]);
  }

   onCancel() : void {
    this.navigateToTransactions();
  }

   onSubmit() : void {
    if (this.existed) {
      this.transactionService.updateTransaction(this.transaction).subscribe(p => this.navigateToTransactions());
    } else {
      this.transactionService.addTransaction(this.transaction).subscribe(p => this.navigateToTransactions());
    }
  }

   onDelete() : void {
    this.transactionService.setTransactionStatus(this.transaction.transactionId, true).subscribe(s => this.transaction.isDeleted = true);
  }

   onUndelete() : void {
    this.transactionService.setTransactionStatus(this.transaction.transactionId, false).subscribe(s => this.transaction.isDeleted = false);
  }

   onPurge() : void {
    this.transactionService.deleteTransaction(this.transaction.transactionId).subscribe(s => this.navigateToTransactions());
  }
}
