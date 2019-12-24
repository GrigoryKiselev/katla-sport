export class Order {
    constructor(
        public id: number,
        public code: string,
        public lastUpdated: string,
        public transationId: string,
        public price: number,
        public isDeleted: boolean
    ) { }
}
