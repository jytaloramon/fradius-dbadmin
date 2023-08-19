import IndexOutOfRangeException from '@/exceptions/IndexOutOfRangeException';

export default class Table<T> {
  private _items: T[];

  private _checkedIndexes: Set<number>;

  public constructor() {
    this._items = new Array();
    this._checkedIndexes = new Set();
  }

  public getItems(): T[] {
    return this._items;
  }

  public appendItems(newItems: T[]): void {
    this._items = this._items.concat(newItems);
  }

  public getCheckedIndexes(): Set<number> {
    return this._checkedIndexes;
  }

  public uncheckAllItems(): void {
    if (this._checkedIndexes.size <= 0) return;

    this._checkedIndexes.clear();
  }

  public checkAllItems(): void {
    if (this._checkedIndexes.size >= this._items.length) return;

    this._checkedIndexes = new Set(Array(this._items.length).keys());
  }

  public removeItemFromChecked(idx: number) {
    if (idx < 0 || idx >= this._items.length)
      throw new IndexOutOfRangeException('indexOutOfRange', {
        min: 0,
        max: this._items.length - 1,
        actual: idx
      });

    if (!this._checkedIndexes.has(idx)) return;

    this._checkedIndexes.delete(idx);
  }

  public addItemToChecked(idx: number) {
    if (idx < 0 || idx >= this._items.length)
      throw new IndexOutOfRangeException('indexOutOfRange', {
        min: 0,
        max: this._items.length - 1,
        actual: idx
      });

    if (this._checkedIndexes.has(idx)) return;

    this._checkedIndexes.add(idx);
  }
}
