import { expect, describe, test } from 'vitest';
import Table from '../../src/models/Table';

describe('Append list to table', () => {
  test('Empty', () => {
    const table = new Table<string>();

    expect(table.getItems().length).toBe(0);
  });

  test('A list', () => {
    const items = ['a', 'b', 'c'];

    const table = new Table<string>();
    table.appendItems(items);

    const actual = new Set(table.getItems());

    expect(actual.size).toBe(items.length);
    expect(actual.has('a') && actual.has('b') && actual.has('c')).toBeTruthy();
  });

  test('Two lists', () => {
    const items1 = ['a', 'b', 'c'];
    const items2 = ['d', 'e'];

    const table = new Table<string>();
    table.appendItems(items1);
    table.appendItems(items2);

    const actual = new Set(table.getItems());

    expect(actual.size).toBe(items1.length + items2.length);
    expect(
      actual.has('a') && actual.has('b') && actual.has('c') && actual.has('d') && actual.has('e')
    ).toBeTruthy();
  });
});

describe('Checked items', () => {
  test('Check all', () => {
    const items = ['a', 'b', 'c'];

    const table = new Table<string>();
    table.appendItems(items);
    table.checkAllItems();

    const actual = table.getCheckedIndexes();

    expect(actual.size).toBe(items.length);
    expect(actual.has(0) && actual.has(1) && actual.has(2)).toBeTruthy();
  });

  test('Uncheck all', () => {
    const items = ['a', 'b'];

    const table = new Table<string>();
    table.appendItems(items);
    table.checkAllItems();
    table.uncheckAllItems();

    expect(table.getCheckedIndexes().size).toBe(0);
  });

  test('Adds an item with negative index', () => {
    const items = ['a', 'b'];

    const table = new Table<string>();
    table.appendItems(items);

    expect(() => {
      table.addItemToChecked(-1);
    }).toThrowError('indexOutOfRange');
  });

  test('Adds an item with index equal to the list size', () => {
    const items = ['a', 'b'];

    const table = new Table<string>();
    table.appendItems(items);

    expect(() => {
      table.addItemToChecked(items.length);
    }).toThrowError('indexOutOfRange');
  });

  test('Adds an item with index greate than the list size', () => {
    const items = ['a', 'b'];

    const table = new Table<string>();
    table.appendItems(items);

    expect(() => {
      table.addItemToChecked(items.length + 1);
    }).toThrowError('indexOutOfRange');
  });

  test('Add items to checked', () => {
    const items = ['a', 'b', 'c'];

    const table = new Table<string>();
    table.appendItems(items);
    table.addItemToChecked(0);
    table.addItemToChecked(2);

    const actual = table.getCheckedIndexes();

    expect(actual.size).toBe(2);
    expect(actual.has(0) && actual.has(2)).toBeTruthy();
  });

  test('Remove with empty list', () => {
    const table = new Table<string>();

    expect(() => {
      table.removeItemFromChecked(0);
    }).toThrowError('indexOutOfRange');
  });

  test('Remove an item with negative index', () => {
    const items = ['a', 'b', 'c'];

    const table = new Table<string>();
    table.appendItems(items);

    expect(() => {
      table.removeItemFromChecked(-1);
    }).toThrowError('indexOutOfRange');
  });

  test('Remove items from checked', () => {
    const items = ['a', 'b', 'c'];

    const table = new Table<string>();
    table.appendItems(items);
    table.checkAllItems();
    table.removeItemFromChecked(0);
    table.removeItemFromChecked(2);

    const actual = table.getCheckedIndexes();

    expect(actual.size).toBe(1);
    expect(actual.has(1)).toBeTruthy();
  });
});
