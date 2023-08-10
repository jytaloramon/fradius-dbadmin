export default abstract class Exception {
  private _msg: string;

  public constructor(msg: string) {
    this._msg = msg;
  }

  public get msg(): string {
    return this._msg;
  }
}
