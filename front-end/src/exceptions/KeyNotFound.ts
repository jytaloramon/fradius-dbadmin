import Exception from './Exception';

export default class KeyNotFound extends Exception {
  public constructor(msg: string) {
    super(msg);
  }
}
