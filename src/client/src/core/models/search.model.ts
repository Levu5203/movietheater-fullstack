export class SearchModel {
  public keyword!: string;
  public pageNumber!: number;
  public pageSize!: number;
  public orderBy!: string;
  public orderDirection!: OrderDirection;
  public includeInactive!: boolean;
}

export enum OrderDirection {
  ASC = 0,
  DESC = 1,
}
