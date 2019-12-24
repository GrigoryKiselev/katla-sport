export class TransactionListItem {
    constructor(
        public transactionId: number,
        public bankAccountId: string,
        public paymentDate: string,
        public totalSumm: string,
        public storeOrderId: number,
        public isDeleted: boolean
    ) { }
}
