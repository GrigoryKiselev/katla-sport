export class OrderListItem {
    constructor(
        public id: number,
        public code: string,
        public transationId: string,
        public price: number,
        public isDeleted: boolean
    ) { }
}
