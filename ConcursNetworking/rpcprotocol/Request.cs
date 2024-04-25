using System;

namespace ConcursNetwork.rpcprotocol; 

[Serializable]
public class Request {
    private RequestType type;
    private object data;

    private Request() {}

    public RequestType Type {
        get { return type; }
    }

    public object Data {
        get { return data; }
    }

    private void SetType(RequestType type) {
        this.type = type;
    }

    private void SetData(object data) {
        this.data = data;
    }

    public override string ToString() {
        return $"Request{{type='{type}', data='{data}'}}";
    }

    public class Builder {
        private Request request = new Request();

        public Builder Type(RequestType type) {
            request.SetType(type);
            return this;
        }

        public Builder Data(object data) {
            request.SetData(data);
            return this;
        }

        public Request Build() {
            return request;
        }
    }
    
    
}