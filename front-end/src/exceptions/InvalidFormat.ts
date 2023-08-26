import Exception from './Exception';

export default class InvalidFormat extends Exception {
  public constructor(msg: string) {
    super(msg);
  }
}
