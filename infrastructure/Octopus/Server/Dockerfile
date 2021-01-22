FROM octopusdeploy/octopusdeploy:2020.5

RUN curl -s https://packages.cloud.google.com/apt/doc/apt-key.gpg | apt-key add - && \
    echo "deb https://apt.kubernetes.io/ kubernetes-xenial main" | tee -a /etc/apt/sources.list.d/kubernetes.list && \
    apt-get update && apt-get install -y kubectl=1.20.0-00
    
RUN curl https://raw.githubusercontent.com/helm/helm/master/scripts/get-helm-3 | bash