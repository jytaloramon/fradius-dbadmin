import Exception from './Exception';

export default class IndexOutOfRangeException extends Exception {
  public constructor(msg: string) {
    super(msg);
  }
}
