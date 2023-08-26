export interface IParametersException {
  [key: string]: Object;
}

export default abstract class Exception extends Error {
  public constructor(
    private _messageKey: string,
    private _parameters?: IParametersException
  ) {
    super(_messageKey);
  }

  public getMessageKey(): string {
    return this._messageKey;
  }

  public getParameters(): IParametersException | undefined {
    return this._parameters;
  }
}
