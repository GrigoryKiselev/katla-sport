import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TransactionListItem } from '../models/transaction-list-item';
import { OrderService } from '../services/order.service';
import { TransactionService } from '../services/transaction.service';

@Component({
  selector: 'app-transaction-list',
  templateUrl: './transaction-list.component.html'
})
export class TransactionListComponent implements OnInit {

  orderId: number;
  transactions: Array<TransactionListItem>;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private orderService: OrderService, 
    private transactionService: TransactionService
  ) { }

  ngOnInit() {
    this.route.params.subscribe(p => {
      this.orderId = p['id'];
      this.orderService.getTransactions(this.orderId).subscribe(s => this.transactions = s);
    })
  }

  onDelete(transactionId: number) {
    var transactions = this.transactions.find(h => h.transactionId == transactionId);
    this.transactionService.setTransactionStatus(transactionId, true).subscribe(c => transactions.isDeleted = true);
  }

  onUndelete(transactionId: number) {
    var transactions = this.transactions.find(h => h.transactionId == transactionId);
    this.transactionService.setTransactionStatus(transactionId, false).subscribe(c => transactions.isDeleted = false);
  }
}

