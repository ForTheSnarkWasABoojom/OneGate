FROM jetbrains/teamcity-agent:2020.2.1-linux-sudo

RUN curl https://raw.githubusercontent.com/helm/helm/master/scripts/get-helm-3 | bash
RUN helm plugin install https://github.com/chartmuseum/helm-push.git
RUN helm plugin list