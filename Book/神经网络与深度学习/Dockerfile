# 使用 Python 2.7 的官方基础镜像
FROM python:2.7

# 更新包列表并安装必要的软件
RUN apt-get update && \
    apt-get install -y --no-install-recommends \
    build-essential \
    python-dev \
    python-pip

# 安装 JupyterLab 和所需的 Python 包
RUN pip install --upgrade pip && \
    pip install jupyterlab numpy scipy matplotlib

# 设置工作目录
WORKDIR /workspace

# 开放 JupyterLab 的默认端口
EXPOSE 8888

# 启动 JupyterLab
CMD ["jupyter", "lab", "--ip=0.0.0.0", "--port", "8888", "--allow-root", "--no-browser"]