import Exception, { type IParametersException } from './Exception';

export default class IndexOutOfRangeException extends Exception {
  public constructor(messageKey: string, parameters?: IParametersException) {
    super(messageKey, parameters);
  }
}
