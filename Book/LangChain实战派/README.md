# README

```bash
# 系统环境
alias mygitauto='git add --all && git commit -a -m $(TZ=UTC-8 date +"%Y%m%d-%H%M%S") && git push'
alias mygitclone='git -c http.proxy="socks5://192.168.0.9:1080" -c https.proxy="socks5://192.168.0.9:1080" clone'
alias mygitpush='git -c http.proxy="socks5://192.168.0.9:1080" -c https.proxy="socks5://192.168.0.9:1080" push'
alias mygitpull='git -c http.proxy="socks5://192.168.0.9:1080" -c https.proxy="socks5://192.168.0.9:1080" pull'
apt-get install -y python3-full python3-pip
# 安装ollama
curl -fsSL https://ollama.com/install.sh | sh
ollama -v
ollama pull gpt-oss:20b

# 图书
# [LangChain实战派.pdf](./assets/file/LangChain实战派.pdf)

# 项目代码
mkdir -p /root/GithubProjects && cd /root/GithubProjects/
git clone git@github.com:michaelbrucelin/HelloAI.git
cd HelloAI/Book/LangChain实战派

# Python环境
cd /root/GithubProjects/HelloAI/Book/LangChain实战派/
python3 -m venv venv
venv/bin/pip3 install jupyterlab
venv/bin/jupyter lab --allow-root --no-browser --port 8888 --ip=192.168.88.12

venv/bin/pip3 install --upgrade langchain-core langchain-community langchain-openai
venv/bin/pip3 install --upgrade ollama
venv/bin/pip3 install --upgrade loguru numexpr

# 运行Jupyter
cd /root/GithubProjects/HelloAI/Book/LangChain实战派/
venv/bin/jupyter lab --allow-root --no-browser --port 8888 --ip=192.168.88.12

# 使用包
import numpy as langchain
```
